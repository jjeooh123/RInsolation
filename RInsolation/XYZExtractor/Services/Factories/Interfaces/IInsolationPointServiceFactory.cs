using Autodesk.Revit.DB;

namespace Insolation.XYZExtractor
{
    /// <summary>
    /// Factory for creating instances of <see cref="IInsolationPointService"/>.
    /// Intended for use in client command initialization blocks, where the factory
    /// itself is typically created during the application's main Init method
    /// by default constructor.
    /// </summary>
    public interface IInsolationPointServiceFactory
    {
        /// <summary>
        /// Creates a new <see cref="IInsolationPointService"/> instance.
        /// </summary>
        /// <param name="document">The Revit document context.</param>
        /// <returns>An initialized <see cref="IInsolationPointService"/>.</returns>
        /// <remarks>
        /// TODO: consider refactoring to a parameterless
        /// <c>Create()</c> method where the extractor configuration occurs in the factory's constructor
        /// or in separate factory implementations.
        /// </remarks>
        public IInsolationPointService Create(Document document); // TODO Create(void)?
    }

}
