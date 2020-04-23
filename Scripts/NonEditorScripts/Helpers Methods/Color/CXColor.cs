using UnityEngine;

namespace CXUtils.CodeUtils
{
    ///<summary> Cx's Color Class </summary>
    public struct ColorUtils
    {
        #region Script Methods
        ///<summary> Mapping A Color In A Range To Another Range </summary>
        public static Color Map(Color startColor, Color RangeColor_Min, Color RangeColor_Max,
            Color MapToColor_Min, Color MapToColor_Max) =>
            new Color
            {
                r = MathUtils.Map(startColor.r, RangeColor_Min.r, RangeColor_Max.r, MapToColor_Min.r, MapToColor_Max.r),
                g = MathUtils.Map(startColor.g, RangeColor_Min.g, RangeColor_Max.g, MapToColor_Min.g, MapToColor_Max.g),
                b = MathUtils.Map(startColor.b, RangeColor_Min.b, RangeColor_Max.b, MapToColor_Min.b, MapToColor_Max.b),
                a = MathUtils.Map(startColor.a, RangeColor_Min.a, RangeColor_Max.a, MapToColor_Min.a, MapToColor_Max.a)
            };
        #endregion
    }
}