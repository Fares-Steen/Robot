using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Threading;
using System.Threading.Tasks;
namespace RobotVS
{
 class Moving {
         int TRIG=17;
         int ECHO=4;
       
         public long distance{get; set;}
         public bool canMove =false;

        public bool notStop = true;
        public bool goingBack = false;
        public DateTime goingBackTime = DateTime.Now;


        private int[] ControlPin = new int[] { 6, 13, 19, 26 };
        int[,] seg = new int[,] {
                          {1, 0, 0, 0 },
                          {1, 1, 0, 0},
                          {0, 1, 0, 0},
                          {0, 1, 1, 0},
                          {0, 0, 1, 0},
                          {0, 0, 1, 1},
                          {0, 0, 0, 1},
                          {1, 0, 0, 1}
                    };

     GpioController gpio = new GpioController(PinNumberingScheme.Logical, new RaspberryPi3Driver());

    public  void Run(){
        InitMotor();
        Task task1 = Task.Factory.StartNew(() => StartSonic());
        Task task2 = Task.Factory.StartNew(() => MoveMotor());
        // StartSonic();
        // MoveMotor();
        Task.WaitAll(task1, task2);
    }
       public void StartSonic(){
       Console.WriteLine("Start Sonic");
        for (int x=0; x<1000;x++){
            
                 gpio.OpenPin(TRIG, PinMode.Output);
                    gpio.Write(TRIG,0);

                 gpio.OpenPin(ECHO, PinMode.Input);

                Thread.Sleep(TimeSpan.FromSeconds(0.1));
                var startScanning = DateTime.Now.AddSeconds(2);
                
                gpio.Write(TRIG,1);
                 Thread.Sleep(TimeSpan.FromSeconds(0.1));
                 Boolean worked=true;
                gpio.Write(TRIG,0);
                

                    while (gpio.Read(ECHO)==0)
                    {
                        
                      
                        if( DateTime.Now>startScanning){
                             Console.WriteLine("Failed aftre 2 sec");
                            gpio.ClosePin(ECHO);
                             gpio.ClosePin(TRIG);
                             worked=false;
                             break;
                        }
                    }
                    if(worked){
                     var start = DateTime.Now;

                     while (gpio.Read(ECHO)==1)
                    {

                    }
                     var stop = DateTime.Now;
                    //Console.WriteLine(stop.Ticks);

                   distance=((stop.Ticks - start.Ticks) *17)/10000;
                   Console.WriteLine(distance);

                   gpio.ClosePin(ECHO);
                   gpio.ClosePin(TRIG);
                    }
            }
            EndAll();
            }
        

        public void InitMotor(){
            Console.WriteLine("Init Motor");
            EndAll();
            notStop = true;
                 for (int pin = 0; pin < 4; pin++)
                {
                    gpio.OpenPin(ControlPin[pin], PinMode.Output);

                }
        }
       public void MoveMotor(){
            Console.WriteLine("Move Motor");
            while (notStop)
                {
                if (distance > 10&&!goingBack)
                {
                    for (int halfstep = 7; halfstep > 0; halfstep--)
                    {
                        for (int pin = 0; pin < 4; pin++)
                        {
                            if (notStop)
                                gpio.Write(ControlPin[pin], seg[halfstep, pin]);
                            Thread.Sleep(TimeSpan.FromSeconds(0.0011));
                        }

                    }
                }
                else {
                    goingBack = true;
                    if (distance < 20)
                    {
                        for (int halfstep = 0; halfstep < 8; halfstep++)
                        {
                            for (int pin = 0; pin < 4; pin++)
                            {
                                if (notStop)
                                    gpio.Write(ControlPin[pin], seg[halfstep, pin]);
                                Thread.Sleep(TimeSpan.FromSeconds(0.0011));
                            }

                        }
                    }
                    else {
                        goingBack = false;
                    }
                }
                   

                }
       }

     

   

       public void EndAll(){
           notStop=false;
            Console.WriteLine("Ending All");
            for (int pin = 0; pin < 4; pin++)
                {
                try
                {
                    gpio.ClosePin(ControlPin[pin]);

                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }


                }
               
       }

    }
}