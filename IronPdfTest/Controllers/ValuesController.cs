using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
using IronPdf;

namespace IronPdfTest.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            var renderer = new HtmlToPdf(new PdfPrintOptions
            {
                PaperSize = PdfPrintOptions.PdfPaperSize.A4,
                MarginTop = 5,
                MarginBottom = 10,
                MarginLeft = 5,
                MarginRight = 5,
                Header = new HtmlHeaderFooter
                {
                    HtmlFragment = "<div style='text-align:right;font-size:10px;'>Payments Report page {page} of {total-pages}. Generated: {date} {time}</div>"
                }
            });

            var html = File.ReadAllText($@"{HostingEnvironment.ApplicationPhysicalPath}/sample.html");
            var pdf = renderer.RenderHtmlAsPdf(html);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdf.BinaryData)
            };

            response.Content.Headers.ContentLength = pdf.BinaryData.Length;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Iron_Pdf_Sample.pdf"
            };

            return ResponseMessage(response);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
