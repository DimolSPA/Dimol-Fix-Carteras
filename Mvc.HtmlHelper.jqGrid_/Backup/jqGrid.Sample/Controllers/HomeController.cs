using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Mvc.HtmlHelpers;
using jqGrid.Sample.Models;

namespace jqGrid.Sample.Controllers
{
    public class HomeController : Controller
    {
        #region Page Controllers
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            if (!string.IsNullOrEmpty(Request.QueryString["TabIndex"]))
            {
                string tab = Request.QueryString["TabIndex"];
                ViewBag.TabIndex = tab;
                ViewBag.SelectedTab = Convert.ToInt32(tab) - 1;
            }
            else
            {
                ViewBag.TabIndex = "1";
                ViewBag.SelectedTab = "0";
            }

            HomeModel model = new HomeModel();
            model.GridSelect = "1:USA;2:England;3:France;4:Germany";
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(HomeModel model)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Account> accountList = serializer.Deserialize<List<Account>>(model.GridData);

                // Process returned data here
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }
        #endregion

        #region Page Actions
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAccountList(GridSettings gridSettings)
        {
            // create json data
            int totalRecords = 1000;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Account> list = new List<Account>();
            for (int iX = startRow; iX < endRow; iX++)
            {
                Account account = new Account();
                account.AccountNumber = string.Format("Account# {0}", iX);
                account.AccountText = "Line# " + iX.ToString();
                account.AccountDropdown = ((Convert.ToSingle(iX) % 2 == 0) ? "USA" : "England");
                account.AccountDate = DateTime.Now.ToShortDateString();
                account.AccountType = ((Convert.ToSingle(iX) % 2 == 0) ? "Yes" : "No");
                account.AccountBalance = "12345678.90";
                list.Add(account);
            }

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Account item in list
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.AccountNumber,
                            item.AccountText,
                            item.AccountDropdown,
                            item.AccountDate,
                            item.AccountType,
                            item.AccountBalance
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomGridDetails(string rowId)
        {
            List<AccountDetail> details = GetAccountDetail(rowId);
            return View("_CustomGridDetails", details);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CustomTree(GridSettings gridSettings, string rowId)
        {
            List<Organization> list = GetOrganization(rowId);

            int pageIndex = gridSettings.pageIndex;
            int pageSize = gridSettings.pageSize;
            int startRow = ((pageIndex - 1) * pageSize) + 1;
            int endRow = startRow + pageSize;
            int totalRecords = list.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = pageIndex,
                records = totalRecords,

                rows =
                (
                    from Organization o in list
                    select new
                    {
                        id = o.Id,
                        cell = new object[]
                        {
                            o.Name,
                            o.AssetAllocation,
                            o.ClientReview,
                            o.IneligibleActivity,
                            o.InvestmentGuideline,
                            o.BillableAssets,
                            o.NonEarnedPcs
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CustomTreeLevel(string rowId)
        {
            string[] split = rowId.Split('_');
            ViewData["RowId"] = rowId;

            if (split[0] == "3")
            {
                // level == 3
                return View("_CustomTreeDetails");
            }
            else
            {
                List<Organization> list = GetOrganization(rowId);
                return View("_CustomTreeLevel", list);
            }
        }
        #endregion

        #region Database Classes
        public class Account
        {
            public int Id { get; set; }
            public string AccountNumber { get; set; }
            public string AccountText { get; set; }
            public string AccountDropdown { get; set; }
            public string AccountDate { get; set; }
            public string AccountType { get; set; }
            public string AccountBalance { get; set; }
        }

        public class AccountDetail
        {
            public string AccountId { get; set; }
            public string CLIENT_NAME { get; set; }
            public string FA_NO { get; set; }
            public int NO_OF_ACCTS { get; set; }
            public decimal TOTAL_ASSETS { get; set; }
            public decimal CASH { get; set; }
            public decimal MSG_FEES { get; set; }
            public double MSG_FEE_PCT { get; set; }
            public string PORTFOLIO_NAME { get; set; }
            public List<PortfolioDetail> PortfolioDetails = new List<PortfolioDetail>();
        }

        public class PortfolioDetail
        {
            public string AccountId { get; set; }
            public string ACCOUNT_NAME { get; set; }
            public string ACCOUNT_TYPE { get; set; }
            public string PROGRAM { get; set; }
            public string INVESTMENT { get; set; }
            public decimal TOTAL_ASSETS { get; set; }
            public decimal CASH { get; set; }
            public decimal MSG_FEES { get; set; }
            public double MSG_FEE_PCT { get; set; }
        }

        public class Details
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Client { get; set; }
            public int AssetAllocation { get; set; }
            public int ClientReview { get; set; }
            public int IneligibleActivity { get; set; }
            public int InvestmentGuideline { get; set; }
            public decimal BillableAssets { get; set; }
            public decimal NonEarnedPcs { get; set; }
        }
        #endregion

        #region Private Methods
        private List<AccountDetail> GetAccountDetail(string AccountId)
        {
            List<AccountDetail> list = new List<AccountDetail>();
            AccountDetail acct1 = new AccountDetail()
            {
                AccountId = "1A",
                CLIENT_NAME = "Bethel, Alexander",
                PORTFOLIO_NAME = "Long Term (Modereately Conservative)",
                FA_NO = "2857236",
                NO_OF_ACCTS = 5,
                TOTAL_ASSETS = 2098492M,
                CASH = 184182M,
                MSG_FEES = 24459M,
                MSG_FEE_PCT = 1.50D
            };
            acct1.PortfolioDetails.Add(new PortfolioDetail()
            {
                AccountId = "1A",
                ACCOUNT_NAME = "123-45678",
                ACCOUNT_TYPE = "IRA",
                PROGRAM = "FIRM",
                INVESTMENT = "ALLIANCE STRAT RSRCH-MAA",
                TOTAL_ASSETS = 550372M,
                CASH = 23145M,
                MSG_FEES = 7235M,
                MSG_FEE_PCT = 1.50D
            });
            acct1.PortfolioDetails.Add(new PortfolioDetail()
            {
                AccountId = "1A",
                ACCOUNT_NAME = "123-456785",
                ACCOUNT_TYPE = "SEP",
                PROGRAM = "FIRM",
                INVESTMENT = "4 MF/ETF/MGR UDPS MODERATE",
                TOTAL_ASSETS = 386888M,
                CASH = 5810M,
                MSG_FEES = 4248M,
                MSG_FEE_PCT = 1.50D
            });
            list.Add(acct1);
            AccountDetail acct2 = new AccountDetail()
            {
                AccountId = "2B",
                CLIENT_NAME = "Smith, Melissa",
                PORTFOLIO_NAME = "Vacation (Moderately Aggressive)",
                FA_NO = "2857236",
                NO_OF_ACCTS = 6,
                TOTAL_ASSETS = 14819174M,
                CASH = 178183M,
                MSG_FEES = 140732M,
                MSG_FEE_PCT = 1.00D
            };
            acct2.PortfolioDetails.Add(new PortfolioDetail()
            {
                AccountId = "2B",
                ACCOUNT_NAME = "53X-57K92",
                ACCOUNT_TYPE = "IRA",
                PROGRAM = "FIRM",
                INVESTMENT = "6 MF/ETF/MGR UDP MODERATE TE",
                TOTAL_ASSETS = 550372M,
                CASH = 23145M,
                MSG_FEES = 7235M,
                MSG_FEE_PCT = 1.50D
            });
            acct2.PortfolioDetails.Add(new PortfolioDetail()
            {
                AccountId = "2B",
                ACCOUNT_NAME = "53X-14L08",
                ACCOUNT_TYPE = "IRA",
                PROGRAM = "NON",
                INVESTMENT = "Moderately Convervative Multi-assets",
                TOTAL_ASSETS = 386888M,
                CASH = 5810M,
                MSG_FEES = 4248M,
                MSG_FEE_PCT = 1.50D
            });
            list.Add(acct2);
            return list;
        }

        private List<Organization> GetOrganization(string rowId)
        {
            List<Organization> list = new List<Organization>();
            string level = string.IsNullOrEmpty(rowId) ? "" : rowId.Split('_')[0];
            switch (level)
            {
                default:
                    {
                        #region Level root
                        ////////////////////////////////////////////////////////////////////////////
                        list.Add(new Organization()
                        {
                            Id = "0_1",
                            Parent = rowId,
                            Name = "South Atlantic (05CARL)",
                            AssetAllocation = 10,
                            ClientReview = 12,
                            IneligibleActivity = 22,
                            InvestmentGuideline = 16,
                            BillableAssets = 333000000M,
                            NonEarnedPcs = 37000M
                        });
                        ////////////////////////////////////////////////////////////////////////////
                        #endregion
                        break;
                    }
                case "0":
                    {
                        #region Level 1
                        ////////////////////////////////////////////////////////////////////////////
                        list.Add(new Organization()
                        {
                            Id = "1_1",
                            Parent = rowId,
                            Name = "COLUMBIA SC (726)",
                            AssetAllocation = 0,
                            ClientReview = 0,
                            IneligibleActivity = 0,
                            InvestmentGuideline = 0,
                            BillableAssets = 100000000M,
                            NonEarnedPcs = 0M
                        });
                        list.Add(new Organization()
                        {
                            Id = "1_2",
                            Parent = rowId,
                            Name = "CHARLOTTE & ASSOCIATES (728)",
                            AssetAllocation = 1,
                            ClientReview = 1,
                            IneligibleActivity = 1,
                            InvestmentGuideline = 1,
                            BillableAssets = 100000000M,
                            NonEarnedPcs = 0M
                        });
                        list.Add(new Organization()
                        {
                            Id = "1_3",
                            Parent = rowId,
                            Name = "GREATER CAROLINA COMPLEX (746)",
                            AssetAllocation = 6,
                            ClientReview = 7,
                            IneligibleActivity = 18,
                            InvestmentGuideline = 11,
                            BillableAssets = 33000000M,
                            NonEarnedPcs = 32000M
                        });
                        list.Add(new Organization()
                        {
                            Id = "1_4",
                            Parent = rowId,
                            Name = "HAMPTON ROADS (755)",
                            AssetAllocation = 3,
                            ClientReview = 4,
                            IneligibleActivity = 3,
                            InvestmentGuideline = 3,
                            BillableAssets = 100000000M,
                            NonEarnedPcs = 5000M
                        });
                        ////////////////////////////////////////////////////////////////////////////
                        #endregion
                        break;
                    }
                case "1":
                    {
                        #region Level 2
                        ////////////////////////////////////////////////////////////////////////////
                        list.Add(new Organization()
                        {
                            Id = "2_1",
                            Parent = rowId,
                            Name = "HIGH POINT, NC (2EQ)",
                            AssetAllocation = 1,
                            ClientReview = 1,
                            IneligibleActivity = 2,
                            InvestmentGuideline = 1,
                            BillableAssets = 10000000M,
                            NonEarnedPcs = 10000M
                        });
                        list.Add(new Organization()
                        {
                            Id = "2_2",
                            Parent = rowId,
                            Name = "LENOR, NC (2S8)",
                            AssetAllocation = 0,
                            ClientReview = 0,
                            IneligibleActivity = 2,
                            InvestmentGuideline = 1,
                            BillableAssets = 10000000M,
                            NonEarnedPcs = 10000M
                        });
                        list.Add(new Organization()
                        {
                            Id = "2_3",
                            Parent = rowId,
                            Name = "HENDERSONVILLE, NC (313)",
                            AssetAllocation = 4,
                            ClientReview = 4,
                            IneligibleActivity = 10,
                            InvestmentGuideline = 8,
                            BillableAssets = 3000000M,
                            NonEarnedPcs = 10000M
                        });
                        list.Add(new Organization()
                        {
                            Id = "2_4",
                            Parent = rowId,
                            Name = "ASHVILLE, NC (702)",
                            AssetAllocation = 1,
                            ClientReview = 2,
                            IneligibleActivity = 4,
                            InvestmentGuideline = 1,
                            BillableAssets = 10000000M,
                            NonEarnedPcs = 2000M
                        });
                        ////////////////////////////////////////////////////////////////////////////
                        #endregion
                        break;
                    }
                case "2":
                    {
                        #region Level 3
                        ////////////////////////////////////////////////////////////////////////////
                        list.Add(new Organization()
                        {
                            Id = "3_1",
                            Parent = rowId,
                            Name = "THE SPG GROUP (3130718)",
                            AssetAllocation = 0,
                            ClientReview = 0,
                            IneligibleActivity = 0,
                            InvestmentGuideline = 0,
                            BillableAssets = 1000000M,
                            NonEarnedPcs = 0M
                        });
                        list.Add(new Organization()
                        {
                            Id = "3_2",
                            Parent = rowId,
                            Name = "GRICE WARD SURRETT T (3131056)",
                            AssetAllocation = 4,
                            ClientReview = 4,
                            IneligibleActivity = 8,
                            InvestmentGuideline = 8,
                            BillableAssets = 1000000M,
                            NonEarnedPcs = 10000M
                        });
                        list.Add(new Organization()
                        {
                            Id = "3_3",
                            Parent = rowId,
                            Name = "ROBERT MACDONALD (3131086)",
                            AssetAllocation = 0,
                            ClientReview = 0,
                            IneligibleActivity = 2,
                            InvestmentGuideline = 0,
                            BillableAssets = 1000000M,
                            NonEarnedPcs = 0M
                        });
                        ////////////////////////////////////////////////////////////////////////////
                        #endregion
                        break;
                    }
            }
            return list;
        }

        private List<Details> GetOrgDetails(string rowId)
        {
            List<Details> list = new List<Details>();
            list.Add(new Details()
            {
                Id = "1",
                Name = "Long Term",
                Client = "Heil, Suzzane",
                AssetAllocation = 5,
                ClientReview = 0,
                IneligibleActivity = 2,
                InvestmentGuideline = 0,
                BillableAssets = 591080M,
                NonEarnedPcs = 7674M
            });
            list.Add(new Details()
            {
                Id = "2",
                Name = "29S0542",
                BillableAssets = 337992.22M,
                NonEarnedPcs = 1100M
            });

            return list;
        }
        #endregion
    }
}
