﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                var meta = (bs.Metadata as BitmapMetadata);
                foreach (var key in _metadata.Keys.ToArray())
                {
                    _metadata[key] = meta.GetQuery(key);
                }
            }
        }

        public Dictionary<string, object> Metadata { get { return _metadata; } }
    }
}
