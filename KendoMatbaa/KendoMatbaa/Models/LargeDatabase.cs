using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
//using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;


namespace KendoMatbaa.Models
{
    using KendoMatbaa.Models;
    using DevExpress.Web.Mvc;

    public static class LargeDatabaseDataProvider
    {
        const string LargeDatabaseDataContextKey = "DXLargeDatabaseDataContext";

        public static matbaaEntities DB
        {
            get
            {
                if (HttpContext.Current.Items[LargeDatabaseDataContextKey] == null)
                    HttpContext.Current.Items[LargeDatabaseDataContextKey] = new matbaaEntities();
                return (matbaaEntities)HttpContext.Current.Items[LargeDatabaseDataContextKey];
            }
        }

        public static IQueryable<IsEmri> Emails { get { return DB.IsEmri; } }

        public static object GetPersonsRange(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            return (from person in DB.IsEmri
                    where (person.IsEmriId + " " + person.IsNo+ " " + person.Adi).StartsWith(args.Filter)
                    orderby person.IsEmriId
                    select person
                    ).Skip(skip).Take(take);
        }
        public static object GetPersonByID(ListEditItemRequestedByValueEventArgs args)
        {
            int id;
            if (args.Value == null || !int.TryParse(args.Value.ToString(), out id))
                return null;
            return DB.IsEmri.Where(p => p.IsEmriId == id).Take(1);
        }
    }}