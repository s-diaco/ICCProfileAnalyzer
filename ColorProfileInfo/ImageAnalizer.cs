using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurigma.GraphicsMill;
using Aurigma.GraphicsMill.Transforms;
using Aurigma.GraphicsMill.AdvancedDrawing;

namespace ColorProfileInfo
{
    public class ImageAnalizer
    {
        public string GetChannelIds(System.IO.StreamReader sr)
        {
            System.IO.Stream stream = sr.BaseStream;

            string retVal = "";
            using (var reader = new Aurigma.GraphicsMill.Codecs.Psd.PsdReader(stream))
            {
                retVal = reader.PixelFormat.ToString();
                foreach (PixelFormat pf in reader.SupportedPixelFormats)
                { 
                    retVal += pf.ToString();
                }
                //read layers and save raster layers in PNG files
                for (int i = 0; i < reader.Frames.Count; i++)
                {
                    using (var frame = reader.Frames[i])
                    {
                        Console.WriteLine("Frame " + frame.Index + " is processing. Frame type is " + frame.Type + ".");

                        if (frame.Type == Aurigma.GraphicsMill.Codecs.Psd.FrameType.Raster)
                        {
                            using (var bitmap = frame.GetBitmap())
                            {
                                bitmap.Save(@"Images\Output\frame_" + i + ".png");
                                retVal += i + ",";
                            }
                        }
                    }
                }
            }
            return retVal;
        }
        public string GetChannelsCount(System.IO.StreamReader sr)
        {
            System.IO.Stream stream = sr.BaseStream;
            string retVal = "";
            using (var bitmap = new Aurigma.GraphicsMill.Bitmap(stream))
            {
                retVal = bitmap.Channels.Count.ToString() + " Channels";
            }
            return retVal;
        }
    }
}
