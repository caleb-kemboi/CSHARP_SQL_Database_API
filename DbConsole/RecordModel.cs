using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DbConsole;


namespace DbConsole
{
    internal class RecordModel
    {
        private readonly Timer _timer;
        public RecordModel()
        {
            _timer = new Timer(600) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            //ServicesDTO.GetNewandChangedTSCNos();

            GeneralSetups.Logger.WriteLog("The Service Is Running" + DateTime.Now);

            Upload();

        }

        public void Start()
        {
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Stop();
        }

        public static void Upload()
        {
            CMSPostbankEntity _db = new CMSPostbankEntity();
            //var records = _db.CMSRecords.Where(m => m.uploaded == 0).ToList();
            List<Contents> contents = new List<Contents>();
            int UploadedColumn = (int)GeneralSetups.Logger.Uploading.uploaded;
            List<CMSRecord> record = _db.CMSRecords.Where(m => m.uploaded == UploadedColumn).ToList();
            Root root = new Root();
            ContentsJson contentsJson = new ContentsJson();


            foreach (var _uploaded in record)
            {
                if (UploadedColumn == 0)
                {
                    Contents content = new Contents()

                    {
                        id = _uploaded.id,
                        batchNumber = _uploaded.batchNumber,
                        uploaded = (int)_uploaded.uploaded,
                        Unit_Cost = (decimal)_uploaded.Unit_Cost,
                        Posting_Status = (string)_uploaded.Posting_Status,
                        TRX_Location = _uploaded.TRX_Location,
                        dateCreated = (DateTime)_uploaded.dateCreated,
                        dateModified = (DateTime)_uploaded.dateModified,
                        ProdId = _uploaded.ProdId,
                        ProdName = _uploaded.ProdName,
                        expiryDate = (DateTime)_uploaded.dateCreated

                    };

                    contents.Add(content);
                }
            }

            contentsJson.contents = contents;

            root.contentsJson = contentsJson;

            string JSON = Newtonsoft.Json.JsonConvert.SerializeObject(root);
            StringContent data = new StringContent(JSON);

            ApiHelper.ApiBody apiBody = ApiHelper.SendRequest("post", "/c00e17d0-22c7-4e13-a312-936137d8e93c", data);


            if (apiBody.code != ApiHelper.CodeMapper.SUCCESS)
            {
                Console.WriteLine("Not Successful");

                //Console.WriteLine("Successful");
                //foreach (CMSRecord ord in uploaded)
                //{
                //    ord.uploaded = 1;
                //    _db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
                //}
                //_db.SaveChanges();
            }
            else
            {
                //mark all as uploaded = 1
                foreach (CMSRecord ord in record)
                {
                    ord.uploaded = 1;
                    _db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
                }
                _db.SaveChanges();

            }


            }
    }
}
