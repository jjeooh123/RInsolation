namespace Insolation.Config
{
    /// <summary>
    /// Defines available strategies for calculating the sun’s position.
    /// </summary>
    public enum SunCoorCalcMethod
    {
        /// <summary>
        /// Use Revit’s built-in <see cref="Autodesk.Revit.DB.SunAndShadowSettings"/>.
        /// </summary>
        RevitSunAndShadowSettings,

        /// <summary>
        /// Use the <c>CoordinateSharp</c> library.
        /// </summary>
        CoordinateSharp
    }
}
