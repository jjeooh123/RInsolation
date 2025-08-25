using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Insolation.Rendering;

namespace Insolation.Commnads
{
    /// <summary>
    /// Command that deletes elements previously created by other commands.
    /// </summary>
    /// <remarks>
    /// Execution flow:
    /// 2. Filters out invalid/deleted elements by matching UniqueId.
    /// 3. Deletes valid elements from document.
    /// 
    /// - TODO: Wrap this in a command wrapper that injects ExternalCommandData and dependencies into context manager
    ///         for configurate services outer the command.
    /// </remarks>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class DeleteCreatedElementsCommand : IExternalCommand
    {
        /// <summary>
        /// Provides factories and shared services needed by the command.
        /// </summary>
        public virtual DeleteCreatedElementsCommandServiceProviderBase ServiceProvider { get; set; } = new DefaultDeleteCreatedElementsCommandServiceProvider();
        
        private UIDocument uidoc;
        private Document doc;
        private List<CreatedElementsInfo> createdElements;

        /// <summary>
        /// Standard Revit IExternalCommand entry point.
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Init(commandData);

            if (createdElements.Count() == 0) return Result.Succeeded;
            // check that the elements to delete still exist in the document
            // and it's UniqueId not changed
            ICollection<ElementId> existionElement = createdElements
                                                        .Where(a => doc.GetElement(a.ElementId)?.UniqueId == a.UniqueId)
                                                        .Select(a => a.ElementId)
                                                        .ToList();

            using (Transaction t = new Transaction(doc, "Remove lines and SketchPlanes"))
            {
                t.Start();
                doc.Delete(existionElement);
                t.Commit();
            }

            // TODO: discard
            TaskDialog.Show("TaskDialog", "вспомогательные построения удалены");

            return Result.Succeeded;
        }

        /// <summary>
        /// Initializes command dependencies via provider and global context.
        /// </summary>
        void Init(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            doc = uidoc.Document;
            var globalContextManager = ServiceProvider.GetIGlobalContextManager();
            createdElements = globalContextManager.GetResult<List<CreatedElementsInfo>>(SharedContextKeys.CreatedElementsId);
        }
    }
}
