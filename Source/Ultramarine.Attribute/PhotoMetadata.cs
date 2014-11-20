using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Media.Imaging;

namespace Ultramarine.Attribute
{
    // TODO: add description here
    // Photo Metadata Policies
    // http://msdn.microsoft.com/en-us/library/windows/desktop/ee872003(v=vs.85).aspx
    public class PhotoMetadata
    {
        readonly Dictionary<string, object> _metadata = new Dictionary<string, object> 
        {
            //{ "System", null },
            { "System.ApplicationName", null },
            { "System.Author", null },
            { "System.Comment", null },
            { "System.Copyright", null },  
            { "System.DateAcquired", null }, 
            { "System.Keywords", null }, 
            { "System.Rating", null },  
            { "System.SimpleRating", null },  
            { "System.Subject", null },  
            { "System.Title", null },

            //{ "System.GPS", null },   
            { "System.GPS.Altitude", null },   
            { "System.GPS.AltitudeRef", null },   
            { "System.GPS.AreaInformation", null },   
            { "System.GPS.Date", null },   
            { "System.GPS.DestBearing", null },   
            { "System.GPS.DestBearingRef", null },   
            { "System.GPS.DestDistance", null },   
            { "System.GPS.DestDistanceRef", null },   
            { "System.GPS.DestLatitude", null },   
            { "System.GPS.DestLatitudeRef", null },   
            { "System.GPS.DestLongitude", null },   
            { "System.GPS.DestLongitudeRef", null },   
            { "System.GPS.Differential", null },   
            { "System.GPS.DOP", null },   
            { "System.GPS.ImgDirection", null },   
            { "System.GPS.ImgDirectionRef", null },   
            { "System.GPS.Latitude", null },   
            { "System.GPS.LatitudeRef", null },   
            { "System.GPS.Longitude", null },   
            { "System.GPS.LongitudeRef", null },   
            { "System.GPS.MapDatum", null },   
            { "System.GPS.MeasureMode", null },   
            { "System.GPS.ProcessingMethod", null },   
            { "System.GPS.Satellites", null },   
            { "System.GPS.Speed", null },   
            { "System.GPS.SpeedRef", null },   
            { "System.GPS.Status", null },   
            { "System.GPS.Track", null },   
            { "System.GPS.TrackRef", null },   
            { "System.GPS.VersionID", null }, 

            //{ "System.Image", null },   
            { "System.Image.ColorSpace", null },   
            { "System.Image.CompressedBitsPerPixel", null },   
            { "System.Image.Compression", null },   
            { "System.Image.HorizontalResolution", null },   
            { "System.Image.ImageID", null },   
            { "System.Image.ResolutionUnit", null },   
            { "System.Image.VerticalResolution", null }, 

            //{ "System.Photo", null },   
            { "System.Photo.Aperture", null },   
            { "System.Photo.Brightness", null },   
            { "System.Photo.CameraManufacturer", null },   
            { "System.Photo.CameraModel", null },   
            { "System.Photo.CameraSerialNumber", null },   
            { "System.Photo.Contrast", null },   
            { "System.Photo.DateTaken", null },   
            { "System.Photo.DigitalZoom", null },   
            { "System.Photo.EXIFVersion", null },   
            { "System.Photo.ExposureBias", null },   
            { "System.Photo.ExposureTime", null },   
            { "System.Photo.Flash", null },   
            { "System.Photo.FlashEnergy", null },   
            { "System.Photo.FlashManufacturer", null },   
            { "System.Photo.FlashModel", null },   
            { "System.Photo.FNumber", null },   
            { "System.Photo.FocalLength", null },   
            { "System.Photo.FocalLengthInFilm", null },   
            { "System.Photo.ISOSpeed", null },   
            { "System.Photo.LensManufacturer", null },   
            { "System.Photo.LensModel", null },   
            { "System.Photo.LightSource", null },   
            { "System.Photo.MakerNote", null },   
            { "System.Photo.MaxAperture", null },   
            { "System.Photo.MeteringMode", null },   
            { "System.Photo.Orientation", null },   
            { "System.Photo.PeopleNames", null },   
            { "System.Photo.PhotometricInterpretation", null },   
            { "System.Photo.ProgramMode", null },   
            { "System.Photo.RelatedSoundFile", null },   
            { "System.Photo.Saturation", null },   
            { "System.Photo.Sharpness", null },   
            { "System.Photo.ShutterSpeed", null },   
            { "System.Photo.SubjectDistance", null },   
            { "System.Photo.TranscodedForSync", null },   
            { "System.Photo.WhiteBalance", null },
        };

        public PhotoMetadata(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                BitmapSource bs = BitmapFrame.Create(fs);
                var meta = (BitmapMetadata)bs.Metadata;
                foreach (var key in _metadata.Keys.ToArray())
                {
                    // ReSharper disable once PossibleNullReferenceException because InvalidCastException will be thrown above
                    _metadata[key] = meta.GetQuery(key);
                }
            }

            TuneCustomObjects();
        }

        private void TuneCustomObjects()
        {
            if (_metadata["System.Photo.DateTaken"] != null)
            {
                var filetime = (FILETIME)_metadata["System.Photo.DateTaken"];
                _metadata["System.Photo.DateTaken"] = FromFileTime(filetime);    
            }

            var ru = (ushort)_metadata["System.Image.ResolutionUnit"];
            _metadata["System.Image.ResolutionUnit"] = (ResolutionUnit) ru;
        }

        private static DateTime FromFileTime(FILETIME filetime)
        {
            return DateTime.FromFileTime(((long)filetime.dwHighDateTime << 32) + (uint)filetime.dwLowDateTime);
        }

        public bool CheckPropertyName(string name)
        {
            return _metadata.ContainsKey(name);
        }

        public bool CheckSubProperties(string propertyPath)
        {
            return _metadata.Keys.FirstOrDefault(k => k.StartsWith(propertyPath + ".")) != null;
        }

        public List<string> GetSubProperties(string propertyPath)
        {
            return _metadata.Keys.Where(key => key.StartsWith(propertyPath + ".")).ToList();
        }

        public Dictionary<string, object> Metadata { get { return _metadata; } }
    }

    public enum ResolutionUnit
    {
        None = 1,
        Inch = 2,
        Centimeter = 3
    }
}
