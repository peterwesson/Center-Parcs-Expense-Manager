using System.Web.Optimization;
using System.Web.Optimization.React;

namespace CenterParcs
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new BabelBundle("~/bundles/transactions").Include(
                      "~/Scripts/person.jsx",
                      "~/Scripts/update-subtransactions.jsx",
                      "~/Scripts/add-subtransaction.jsx",
                      "~/Scripts/add-subtransactions.jsx",
                      "~/Scripts/subtransactions.jsx",
                      "~/Scripts/transaction.jsx",
                      "~/Scripts/add-transaction.jsx",
                      "~/Scripts/update-transaction-modal.jsx",
                      "~/Scripts/transaction-modal.jsx",
                      "~/Scripts/transactions.jsx",
                      "~/Scripts/jsonDecycle.js"));

            bundles.Add(new BabelBundle("~/bundles/summary").Include(
                      "~/Scripts/person.jsx",
                      "~/Scripts/payment-group.jsx",
                      "~/Scripts/summary.jsx",
                      "~/Scripts/jsonDecycle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
