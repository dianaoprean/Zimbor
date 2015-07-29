using System;
using System.Collections.Generic;
using Domain;
using Lib.Web.Mvc.JQuery.JqGrid;
using UI.Admin.Models;

namespace UI.Admin.Extensions
{
    public static class ParsingExtensions
    {
        #region Produs

        public static ProdusModel ToModel(this Produ entity)
        {
            return new ProdusModel
            {
                Id = entity.ID,
                Nume = entity.Nume,
                Descriere = entity.Descriere
            };
        }

        public static void ToEntity(this ProdusModel model, ref Produ entity)
        {
            entity.Nume = model.Nume;
            entity.Descriere = model.Descriere;
            if (model.Id == 0)
            {
                entity.DataAdaugarii = DateTime.Now;
            }
        }

        public static JqGridRecord ToJqGridRecord(this Produ item)
        {
            return new JqGridRecord(Convert.ToString(item.ID), new List<object>
                {
                    item.Nume,
                    item.Descriere,
                    item.DataAdaugarii.ToShortDateString(),
                    item.ID
                });
        }

        #endregion

        #region Client

        public static ClientModel ToModel(this Client entity)
        {
            return new ClientModel
            {
                Id = entity.ID,
                Nume = entity.Nume,
                Prenume = entity.Prenume,
                Adresa = entity.Strada,
                Istoric = entity.Istoric,
                Localitate = entity.Localitate,
                CodPostal = entity.CodPostal,
                JudetId = entity.JudetID
            };
        }

        public static void ToEntity(this ClientModel model, ref Client entity)
        {
            entity.Nume = model.Nume;
            entity.Prenume = model.Prenume;
            entity.Strada = model.Adresa;
            entity.Istoric = model.Istoric;
            entity.Localitate = model.Localitate;
            entity.CodPostal = model.CodPostal;
            entity.JudetID = model.JudetId;
        }

        public static JqGridRecord ToJqGridRecord(this Client item)
        {
            return new JqGridRecord(Convert.ToString(item.ID), new List<object>
                {
                    item.Nume,
                    item.Prenume,
                    item.Strada,
                    item.Localitate,
                    item.CodPostal,
                    item.Judet.Nume,
                    item.Istoric,
                    item.ID
                });
        }

        #endregion

        #region Campanie Marketing

        public static CampanieMarketingModel ToModel(this CampanieMarketing entity)
        {
            return new CampanieMarketingModel
            {
                Id = entity.ID,
                Nume = entity.Nume,
                Descriere = entity.Descriere
            };
        }

        public static void ToEntity(this CampanieMarketingModel model, ref CampanieMarketing entity)
        {
            entity.Nume = model.Nume;
            entity.Descriere = model.Descriere;
            if (model.Id == 0)
            {
                entity.DataAdaugarii = DateTime.Now;
            }
        }

        public static JqGridRecord ToJqGridRecord(this CampanieMarketing item)
        {
            return new JqGridRecord(Convert.ToString(item.ID), new List<object>
                {
                    item.Nume,
                    item.Descriere,
                    item.DataAdaugarii.ToShortDateString(),
                    item.ID
                });
        }

        #endregion

        #region Concurenta

        public static ConcurentaModel ToModel(this Concurenta entity)
        {
            return new ConcurentaModel
            {
                Id = entity.ID,
                Nume = entity.Nume,
                Descriere = entity.Descriere
            };
        }

        public static void ToEntity(this ConcurentaModel model, ref Concurenta entity)
        {
            entity.Nume = model.Nume;
            entity.Descriere = model.Descriere;
            if (model.Id == 0)
            {
                entity.DataAdaugarii = DateTime.Now;
            }
        }

        public static JqGridRecord ToJqGridRecord(this Concurenta item)
        {
            return new JqGridRecord(Convert.ToString(item.ID), new List<object>
                {
                    item.Nume,
                    item.Descriere,
                    item.DataAdaugarii.ToShortDateString(),
                    item.ID
                });
        }


        #endregion

        #region Studiu Piata

        public static StudiuPiataModel ToModel(this StudiuPiata entity)
        {
            return new StudiuPiataModel
            {
                Id = entity.ID,
                Nume = entity.Nume,
                NrTuristi = entity.NrTuristi,
                DataStudiu = entity.DataStudiului,
                Detalii = entity.Detalii
            };
        }

        public static void ToEntity(this StudiuPiataModel model, ref StudiuPiata entity)
        {
            entity.Nume = model.Nume;
            entity.NrTuristi = model.NrTuristi;
            entity.DataStudiului = model.DataStudiu;
            entity.Detalii = model.Detalii;
        }

        public static JqGridRecord ToJqGridRecord(this StudiuPiata item)
        {
            return new JqGridRecord(Convert.ToString(item.ID), new List<object>
                {
                    item.Nume,
                    item.NrTuristi.ToString(),
                    item.DataStudiului.ToShortDateString(),
                    item.Detalii,
                    item.ID
                });
        }

        #endregion

        #region Primire

        public static PrimireTuristicaModel ToModel(this PrimireTuristica entity)
        {
            return new PrimireTuristicaModel
            {
                Id = entity.ID,
                Nume = entity.Nume,
                Strada = entity.Strada,
                Localitate = entity.Localitate,
                CodPostal = entity.CodPostal,
                Telefon = entity.Telefon,
                NumeProprietar = entity.NumeProprietar,
                ZonaId = entity.ZonaID
            };
        }

        public static void ToEntity(this PrimireTuristicaModel model, ref PrimireTuristica entity)
        {
            entity.Nume = model.Nume;
            entity.Strada = model.Strada;
            entity.ZonaID = model.ZonaId;
            entity.CodPostal = model.CodPostal;
            entity.Telefon = model.Telefon;
            entity.Localitate = model.Localitate;
            entity.NumeProprietar = model.NumeProprietar;
        }

        public static JqGridRecord ToJqGridRecord(this PrimireTuristica item)
        {
            return new JqGridRecord(Convert.ToString(item.ID), new List<object>
                {
                    item.Nume,
                    item.Strada,
                    item.Localitate,
                    item.CodPostal,
                    item.Zona.Nume,
                    item.NumeProprietar,
                    item.Telefon,
                    item.ID
                });
        }

        #endregion
    }
}