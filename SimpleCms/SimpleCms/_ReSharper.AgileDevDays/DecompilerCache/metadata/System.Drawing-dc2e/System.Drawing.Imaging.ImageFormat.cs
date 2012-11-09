// Type: System.Drawing.Imaging.ImageFormat
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Drawing.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime;

namespace System.Drawing.Imaging
{
    [TypeConverter(typeof (ImageFormatConverter))]
    public sealed class ImageFormat
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public ImageFormat(Guid guid);

        public Guid Guid { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat MemoryBmp { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat Bmp { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat Emf { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat Wmf { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat Gif { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat Jpeg { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat Png { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat Tiff { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat Exif { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public static ImageFormat Icon { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public override bool Equals(object o);

        public override int GetHashCode();
        public override string ToString();
    }
}
