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
            using (var bitmap = new Aurigma.GraphicsMill.Bitmap(stream))
            {
                for (int i = 0; i < bitmap.Channels.Count; i++)
                {
                    retVal += bitmap.Channels[i].Id + ",";
                }
                var cyanBitmap = bitmap.Channels[Aurigma.GraphicsMill.Channel.Cyan];
                cyanBitmap.Save(@"Images\Output\Cmyk_test_Channel_C.tif");

                var alphaBitmap = bitmap.Channels[Aurigma.GraphicsMill.Channel.Yellow];
                alphaBitmap.Save(@"Images\Output\Cmyk_test_Channel_A.tif");
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
