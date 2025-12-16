using System;

namespace Station
{
    /// <summary>
    /// When an import event is passed to the application, this indicates the
    /// type of data.
    /// </summary>
    public enum EVENT_STATE
    {
        /// <summary>
        /// The event contains data: an operation and a surgeon.
        /// The fields SurgeonLastName, SurgeonFirstName, OPCode,
        /// OPDescription, OPDateAndTime and OPFunction contain valid data.
        /// </summary>
        STATE_DATA,

        /// <summary>
        /// The event carries information text in StateText which can be displayed.
        /// </summary>
        STATE_INFO,

        /// <summary>
        /// There was an error. The data import continues all the same.
        /// </summary>
        STATE_ERROR
    }

    /// <summary>
    /// The plugin creates an instance of this class, fills it with data,
    /// and fires this event.
    /// <br>An operation is uniquely identified by OPCode and OPDateAndTime</br>
    /// </summary>
    public class ImportEvent
    {
        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName;

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName;

        /// <summary>
        /// The birth date.
        /// </summary>
        public DateTime BirthDate;

        /// <summary>
        /// Set by plugin, read by caller 
        /// </summary>
        public EVENT_STATE State;
        /// <summary>
        /// When State is STATE_INFO or STATE_ERROR, it contains a text with a description.
        /// </summary>
        public string StateText;

        /// <summary>
        /// Set by caller, read by plugin. Notifies the plugin to stop the import. 
        /// </summary>
        public bool Abort;

        /// <summary>
        /// Resets all data in the event. You can create one instance of this class and reset and
        /// fill the data, reusing the instance instead of creating a new instance for every record.
        /// </summary>
        public void ClearData()
        {
            LastName = "";
            FirstName = "";
            BirthDate = new DateTime(1753, 1, 1, 23, 59, 59);
        }
    }

    /// <summary>
    /// Base class for one patient item. A plugin assembly must contain one class derived from this base class.
    /// One instance <code>plugin</code> of the derived class will be instantiated and functions will be called in this order:
    /// <br>
    /// <code>
    /// void ImportDataFromPlugin()
    /// {
    ///     plugin.Import += new StationImport.ImportHandler(o_Import);
    ///     o.ImportInit();
    ///     o.ImportRun();
    ///     o.ImportFinalize();
    /// }
    /// void o_ImportOP(object sender, ImportEvent e)
    /// {
    ///     if (...)
    ///     {
    ///         e.Abort = true;
    ///         Protokoll("Info: Import aborted.");
    ///     }
    ///     else
    ///     {
    ///         switch (e.State)
    ///         {
    ///             case EVENT_STATE.STATE_ERROR:
    ///                 Protokoll("Error:" + e.StateText);
    ///                 break;
    ///
    ///             case EVENT_STATE.STATE_DATA:
    ///                 ImportLine(e);
    ///                 break;
    ///
    ///             case EVENT_STATE.STATE_INFO:
    ///                 Protokoll("Info: " + e.StateText);
    ///                 break;
    ///
    ///             default:
    ///                 break;
    ///         }
    ///     }
    /// }
    /// </code>
    /// </br>
    /// </summary>
    public abstract class StationImport
    {
        /// <summary>
        /// The delegate.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event containing an operation-surgeon item.</param>
        public delegate void ImportHandler(object sender, ImportEvent e);
        /// <summary>
        /// This event is fired for every new record.
        /// </summary>
        public event ImportHandler Import;

        /// <summary>
        /// Fires the event.
        /// </summary>
        /// <param name="e"></param>
        protected void FireImportEvent(ImportEvent e)
        {
            if (Import != null)
            {
                Import(this, e);
            }
        }

        /// <summary>
        /// Is called once at the beginning of the import. Use this perform initialization.
        /// </summary>
        public abstract void ImportInit();

        /// <summary>
        /// Called once after <see cref="ImportInit"/>. This function performs the actual data import and
        /// fires the <see cref="Import"/> event once for every operation-surgeon item.
        /// </summary>
        public abstract void ImportRun();

        /// <summary>
        /// Is called once after <see cref="ImportRun"/>. Use this function to perform any finalzation operations.
        /// </summary>
        public abstract void ImportFinalize();

        /// <summary>
        /// Supplies a description of the plugin which is shown to the user.
        /// </summary>
        /// <returns>The description.</returns>
        public abstract string ImportDescription();
    }
}

