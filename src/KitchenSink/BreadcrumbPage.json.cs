using Starcounter;
using System;
using System.Collections.Generic;

namespace KitchenSink
{
    [Database]
    public class TreeItem
    {
        public string Name;
        public TreeItem Parent;

        public QueryResultRows<TreeItem> Children
        {
            get { return Db.SQL<TreeItem>("SELECT i FROM TreeItem i WHERE Parent = ?", this); }
        }
    }

    partial class BreadcrumbPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            var treeItem = Db.SQL<TreeItem>("SELECT i FROM TreeItem i WHERE Parent IS NOT NULL FETCH ?", 1).First;
            SetActiveItem(treeItem);
        }

        public void SetActiveItem(TreeItem treeItem, bool isAdd = false)
        {
            RebuildBreadcrumb(treeItem, isAdd);
            CurrentTreeItem.Data = treeItem;
            CurrentTreeItem.IsAdd = isAdd;
            CurrentTreeItem.ParentName = treeItem.Parent.Name; 
        }

        public void RebuildBreadcrumb(TreeItem treeItem, bool isAdd)
        {
            var ancestors = new List<TreeItem>();
            var ghostParent = treeItem;

            while (treeItem.Parent != null)
            {
                ancestors.Add(treeItem);
                treeItem = treeItem.Parent;
            }

            ancestors.Reverse();

            Breadcrumbs.Clear();

            if (ancestors.Count > 0)
            {
                Breadcrumbs.Data = ancestors;

                Breadcrumbs[Breadcrumbs.Count - 1].IsActive = true;
                if (isAdd)
                {
                    Breadcrumbs[Breadcrumbs.Count - 1].IsAdd = true;
                }

                var ghost = Breadcrumbs.Add();
                ghost.GhostParent = ghostParent;

                foreach (var breadcrumb in Breadcrumbs)
                {
                    breadcrumb.SearchSiblings("");
                }
            }
        }
    }

    [BreadcrumbPage_json.Breadcrumbs]
    partial class BreadcrumbPageBreadcrumbsElement : Json, IBound<TreeItem>
    {
        public TreeItem GhostParent;

        public string FormattedName
        {
            get
            {
                if (IsAdd)
                {
                    return "Add a new type";
                }
                else if (IsGhost)
                {
                    var count = Db.SlowSQL<Int64>("SELECT COUNT(*) FROM TreeItem i WHERE Parent = ?", ParentItem).First;
                    return count + " children";
                }
                else if (Data != null)
                {
                    return Data.Name;
                }
                else
                {
                    return "";
                }
            }
        }

        public bool IsGhostParentSet
        {
            get { return (GhostParent != null); }
        }

        public TreeItem ParentItem
        {
            get
            {
                if (GhostParent != null)
                {
                    return GhostParent;
                }
                else
                {
                    return Data.Parent;
                }
            }
        }

        void Handle(Input.SelectTrigger action)
        {
            if (Data != null)
            {
                BreadcrumbPage breadcrumbPage = (BreadcrumbPage) Parent.Parent;
                breadcrumbPage.SetActiveItem(Data);
            }
        }

        void Handle(Input.AddSiblingTrigger action)
        {
            BreadcrumbPage breadcrumbPage = (BreadcrumbPage) Parent.Parent;
            TreeItem item = new TreeItem()
            {
                Parent = ParentItem
            };
            breadcrumbPage.SetActiveItem(item, true);
        }

        void Handle(Input.SearchQuery action)
        {
            SearchSiblings(action.Value);
        }

        public void SearchSiblings(string query)
        {
            Siblings.Clear();

            QueryResultRows<TreeItem> result;
            //if (GhostParent != null) {
            if (query.Length > 0)
            {
                result = Db.SQL<TreeItem>("SELECT i FROM TreeItem i WHERE Parent = ? AND Name LIKE ? FETCH ?",
                    ParentItem, query + "%", 5);
            }
            else
            {
                result = Db.SQL<TreeItem>("SELECT i FROM TreeItem i WHERE Parent = ? FETCH ?", ParentItem, 5);
            }
            /*}
            else {
                if (query.Length > 0) {
                    result = Db.SQL<TreeItem>("SELECT i FROM TreeItem i WHERE Parent = ? AND NOT i = ? AND Name LIKE ? FETCH ?", ParentItem, Data, query + "%", 5);
                }
                else {
                    result = Db.SQL<TreeItem>("SELECT i FROM TreeItem i WHERE Parent = ? AND NOT i = ? FETCH ?", ParentItem, Data, 5);
                }
            }*/

            Siblings.Data = result;
        }
    }

    [BreadcrumbPage_json.Breadcrumbs.Siblings]
    partial class BreadcrumbPageBreadcrumbsSiblingsElement : Json, IBound<TreeItem>
    {
        void Handle(Input.SelectTrigger action)
        {
            BreadcrumbPage breadcrumbPage = (BreadcrumbPage) Parent.Parent.Parent.Parent;
            breadcrumbPage.SetActiveItem(Data);
        }
    }

    [BreadcrumbPage_json.CurrentTreeItem]
    partial class BreadcrumbPageCurrentTreeItem : Json, IBound<TreeItem>
    {
        void Handle(Input.SaveTrigger action)
        {
            Transaction.Commit();
            BreadcrumbPage breadcrumbPage = (BreadcrumbPage) Parent;
            breadcrumbPage.SetActiveItem(Data);
        }
    }
}