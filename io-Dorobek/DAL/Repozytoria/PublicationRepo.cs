﻿using io_Dorobek.DAL.Encje;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.ObjectModel;
using io_Dorobek.Model;

namespace io_Dorobek.DAL.Repozytoria
{
    internal class PublicationRepo //rozwazyc statyczna - pozbyc sie ponawiania polaczenia z baza - przechowywac polaczenie
    {
        public List<Publication> getAll()//Do zmiany na PublicationListItem
        {
            using (var database = new DB())
            {
                return database.Publications.ToList();
            }
        }

        public void deleteById(List<uint> Id)
        {
            using (var database = new DB())
            {
                var items = database.Publications.Where(x => Id.Contains(x.Id)).ToList();
                foreach(var item in items)
                    database.Publications.Remove(item);
            }
        }

        public void addPublication(byte[] pdf, PublicationListItem item)
        {
            using(var database = new DB())
            {
                database.Publications.Add(new Publication()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Author = item.Author,
                    Year = item.Year,
                    Doi = item.Doi,
                    PdfFile = pdf
                });
            }
        }

        public (PdfDocument,string) getPdf(uint Id) //rozwazyc liste w wyniku -> zasypywanie bazy zapytaniami przy zapisie wielu -> modyfikacja w fshandlerze
        {
            using (var database = new DB())
            {
                var item = database.Publications.Where(x => x.Id == Id).FirstOrDefault();
                if(null != item)
                {
                    MemoryStream stream = new MemoryStream(item.PdfFile);
                    PdfDocument document = PdfReader.Open(stream, PdfDocumentOpenMode.Import);
                    return (document,$"{item.Author.ToUpper()}_{item.Title.ToUpper()}.pdf");//sprawdzic poprawnosc formatowania - czy zgodna z trescia zadania
                }
                throw new Exception("Nie udało się pobrać pliku z bazy danych.");
            }
        }
    }
}
