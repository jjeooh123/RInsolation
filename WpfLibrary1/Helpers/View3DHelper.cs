using Autodesk.Revit.DB;

namespace Insolation.Helpers
{
    /// <summary>
    /// Provides utilities for retrieving usable <see cref="View3D"/> instances.
    /// </summary>
    /// <remarks>
    /// Currently only checks the active view.
    /// TODO: If the active view is not a <see cref="View3D"/>, create one and
    /// register it for later cleanup.
    /// TODO: Consider refactoring into a service with interfaces, implementations,
    /// and factory support for better testability.
    /// </remarks>
    public static class View3DHelper
    {
        /// <summary>
        /// Gets the current <see cref="View3D"/> from the active document,
        /// if the active view is indeed a <see cref="View3D"/>.
        /// </summary>
        /// <param name="doc">The active Revit document.</param>
        /// <returns>The active <see cref="View3D"/>, or <c>null</c> if not available.</returns>
        public static View3D GetView3D(Document doc) => doc.ActiveView is View3D view3d && view3d is not null ? view3d : null;  // TODO лишняя проверка?
    }
}
