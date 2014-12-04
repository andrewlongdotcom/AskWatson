using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AskWatson.Portable.Test
{
    [TestClass]
    public class WatsonServiceTest
    {
        [TestMethod]
        public async Task ModelUserTest()
        {
            string body = "RT @25point4Podcast  What is @25point4Podcast   Think @mosspuppet meets \"Between Two Ferns\" with @galifianakisz #wpdev's collide  MT @misangenius  How to use @WordPress to display remote notifications in your WP app http //t co/81mheID2Xd via @msicc RT @nathanhehn  Thanks for the donations last month  We raised over $800 for the American Cancer Society  #Wpdev #wpdevsu2c http //t co/lz9… RT @harrymccracken  It's now standard practice for tech support reps to start by declaring that they will definitely solve your problem  Se… via msftresearch Is a Safer Cloud on the Horizon  http //t co/i66tu8i585 Signup for free trial of IBM BlueMix http //t co/pFsygb9YfE and read how to give your #wpdev apps the power of Watson http //t co/ck0mcDP5rz @cpuzder it's the accidents every other mile that's forcing everybody to drive slow this morning At least driving 3 mph makes it really hard to not be careful #mrsunshine JM Ministries  Another 5-star review  http //t co/wQL4vuJ10f #joycemeyer #windowsphone Golf  Jordan Spieth wins Australian Open by 6 shots; 1st American to claim event since B  Faxon (1993) (ESPN) http //t co/hBG8U1yi6u #golf Golf  Jordan Spieth wins Australian Open by 6 strokes; 1st American winner since Brad Faxon in 1993 (ESPN) http //t co/dByZCbrIMt #golf RT @tmckernan  Three #Mizzou #SECCG questions  1  10% 2  0% 3  100% RT @bc3tech  I'm not shaving in Nov to help fight cancer #letitgrow #wpdevsu2c http //t co/RL99w7CxXu #wpdev #day29 http //t co/R1LvFnonmC Final/SO  Blues 3 Wild 2  STL  V Tarasenko 1 goal  MIN  Z Parise 1 goal  STL  J Allen 36 saves  (ESPN) http //t co/PCNiwF4a5A #stlblues    Signup for free trial of IBM BlueMix http //t co/pFsygb9YfE and read how to give your #wpdev apps the power of Watson http //t co/ck0mcDP5rz RT @TheCheekyTaurus  I'm not shaving in Nov to help fight cancer #letitgrow #wpdevsu2c http //t co/os7BO9dJhF #wpdev #day29 http //t co/hoK… in OT  Blues 4 Oilers 3  EDM  D Perron 1 goal  STL  A Pietrangelo 1 goal  EDM  B Scrivens 37 saves  (ESPN) http //t co/dKrazI49nz #stlb    Golf  Rory McIlroy among group tied for 2nd  1 shot behind Greg Chalmers at midpoint of Australian Open (ESPN) http //t co/sA78yMWXax #golf JM Ministries  Another 5-star review  http //t co/XWnV3eQDTT #joycemeyer #windowsphone Happy thanksgiving to all from @nickjonas and @RoyalCaribbean looks like a fun ship  http //t co/mzSZiCejy9";
            AskWatson.Portable.Models.UserModelingResponse.Rootobject response = 
                await AskWatson.Portable.AskWatsonService.ModelUser(body);
            Assert.IsNotNull(response);
        }
    }
}
