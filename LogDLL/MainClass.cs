using LogDLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using LogDLL.WCF;
using System.Threading;

namespace LogDLL
{
    public class MainClass : IOnService
    {
        private List<Thread> list_thread;
        private List<Task> list_task;
        private ConfigClass conf;
        bool stopper;

        public MainClass()
        {
            list_thread = new List<Thread>();
            list_task = new List<Task>();
            stopper = false;
        }

        public void Initialization()
        {
            conf = new ConfigClass();
            //conf.LoadConfig();
            if (conf.GetValue("Logging_IS") == "Да")
            {
                int times = Convert.ToInt32(conf.GetValue("TimeOutLogEmpty"));
                string path = conf.GetValue("PathService");
                WriterLog wr_log = WriterLog.getInstance(path + "\\" +  conf.GetValue("FolderLog"), times);
                Thread newThread = new Thread(wr_log.LOOP);
                list_thread.Add(newThread);
            }    
        }

        public void Start()
        {
            foreach(Thread thread in list_thread)
            {
                thread.Start();
            }
            WCFService wcf = new WCFService(conf);
        }

        public void Curcle()
        {
            Initialization();
            Start();
            /*while (!stopper)
            {
               Thread.Sleep(3000);
            }
            Stop();*/
        }

        public void Stop()
        {
            for(int i=0; i < list_thread.Count;)
            {
                if(list_thread[i].ThreadState == ThreadState.Stopped)
                {
                    i++;
                }
                Thread.Sleep(1000);
            }
        }

        private void AddThread(ThreadStart th_start)
        {
            Thread newThread = new Thread(th_start);
            list_thread.Add(newThread);
        }
    }
}
