using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace XIV.Utils
{
    //https://social.msdn.microsoft.com/Forums/en-US/c873d460-2f7d-41f6-8149-3055a9dd5e7a/how-to-suspend-events-when-setting-a-property-of-a-winforms-control?forum=winforms
    //mct3 named user

    public class EventSuppressor
    {
        private EventHandlerList sourceEventHandlerList;
        private FieldInfo headField;
        private Dictionary<object, Delegate[]> handlers;
        private Timer timer = new Timer();
        private Action timedAction;
        private bool isSuppressed;

        public EventSuppressor(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control", "An instance of a control must be provided.");

            handlers = new Dictionary<object, Delegate[]>();
            var sourceEventsInfo = control.GetType().GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);
            sourceEventHandlerList = (EventHandlerList)sourceEventsInfo.GetValue(control, null);
            headField = sourceEventHandlerList.GetType().GetField("_head", BindingFlags.Instance | BindingFlags.NonPublic);
            timer.Tick += Timer_Tick;
        }

        ~EventSuppressor()
        {
            timer.Tick -= Timer_Tick;
            handlers = null;
            sourceEventHandlerList = null;
            headField = null;
            timer = null;
            timedAction = null;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (timedAction != null)
            {
                timedAction.Invoke();
                timedAction = null;
            }
        }

        private void BuildList()
        {
            object head = headField.GetValue(sourceEventHandlerList);
            if (head != null)
            {
                Type listEntryType = head.GetType();
                FieldInfo delegateField = listEntryType.GetField("_handler", BindingFlags.Instance | BindingFlags.NonPublic);
                FieldInfo keyField = listEntryType.GetField("_key", BindingFlags.Instance | BindingFlags.NonPublic);
                FieldInfo nextField = listEntryType.GetField("_next", BindingFlags.Instance | BindingFlags.NonPublic);
                BuildListWalk(head, delegateField, keyField, nextField);
            }
        }

        private void BuildListWalk(object entry, FieldInfo delegateField, FieldInfo keyField, FieldInfo nextField)
        {
            while (entry != null)
            {
                Delegate dele = (Delegate)delegateField.GetValue(entry);
                if(dele == null)
                {
                    entry = nextField.GetValue(entry);
                    continue;
                }
                object key = keyField.GetValue(entry);
                entry = nextField.GetValue(entry);

                Delegate[] listeners = dele.GetInvocationList();
                if (listeners != null && listeners.Length > 0)
                    handlers.Add(key, listeners);

            }
            //if (entry != null)
            //{
            //    Delegate dele = (Delegate)delegateFI.GetValue(entry);


            //    object key = keyFI.GetValue(entry);
            //    object next = nextFI.GetValue(entry);

            //    if (dele == null)
            //    {
            //        if (next != null)
            //        {
            //            BuildListWalk(next, delegateFI, keyFI, nextFI);
            //        }
            //        return;
            //    }

            //    Delegate[] listeners = dele.GetInvocationList();
            //    if (listeners != null && listeners.Length > 0)
            //        handlers.Add(key, listeners);

            //    if (next != null)
            //    {
            //        BuildListWalk(next, delegateFI, keyFI, nextFI);
            //    }
            //}
        }

        public void Resume(int miliSeconds)
        {
            if (miliSeconds > 0)
            {
                timedAction = Resume;
                timer.Interval = miliSeconds;

                timer.Start();
            }
            else
            {
                Resume();
            }
        }

        public void Resume()
        {
            if (!isSuppressed)
                throw new ApplicationException("Events have not been suppressed.");

            foreach (KeyValuePair<object, Delegate[]> pair in handlers)
            {
                for (int x = 0; x < pair.Value.Length; x++)
                    sourceEventHandlerList.AddHandler(pair.Key, pair.Value[x]);
            }

            handlers.Clear();
            isSuppressed = false;
        }

        public void Suppress(int miliSeconds)
        {
            if (miliSeconds > 0)
            {
                timedAction = Suppress;
                timer.Interval = miliSeconds;

                timer.Start();
            }
            else
            {
                Suppress();
            }
        }

        public void Suppress()
        {
            if (isSuppressed)
                throw new ApplicationException("Events are already being suppressed.");

            BuildList();

            foreach (KeyValuePair<object, Delegate[]> pair in handlers)
            {
                for (int x = pair.Value.Length - 1; x >= 0; x--)
                    sourceEventHandlerList.RemoveHandler(pair.Key, pair.Value[x]);
            }
            isSuppressed = true;
        }
    }
}