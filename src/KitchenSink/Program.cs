using System;
using System.Linq;
using Starcounter;

namespace KitchenSink
{
    class Program
    {
        static void Main()
        {
            var app = Application.Current;
            app.Use(new HtmlFromJsonProvider());
            app.Use(new PartialToStandaloneHtmlProvider());

            DummyData.Create();

            Handle.GET("/KitchenSink/master", () =>
            {
                Session session = Session.Current;
                if (session != null && session.Data != null)
                    return session.Data;

                var master = new MasterPage();

                if (session == null)
                {
                    session = new Session(SessionOptions.PatchVersioning);
                }

                var nav = new NavPage();
                master.CurrentPage = nav;

                master.Session = session;
                return master;
            });

            Handle.GET("/KitchenSink/json", () => { return new Json(); });

            Handle.GET("/KitchenSink/partial/mainpage", () => new MainPage());
            Handle.GET("/KitchenSink/mainpage", () => WrapPage<MainPage>("/KitchenSink/partial/mainpage"));

            Handle.GET("/KitchenSink", () => { return Self.GET("/KitchenSink/mainpage"); });

            Handle.GET("/KitchenSink/partial/button", () => new ButtonPage());
            Handle.GET("/KitchenSink/button", () => WrapPage<ButtonPage>("/KitchenSink/partial/button"));

            Handle.GET("/KitchenSink/partial/breadcrumb",
                () => { return Db.Scope(() => { return new BreadcrumbPage(); }); });
            Handle.GET("/KitchenSink/breadcrumb", () => WrapPage<BreadcrumbPage>("/KitchenSink/partial/breadcrumb"));

            Handle.GET("/KitchenSink/partial/chart", () => new ChartPage());
            Handle.GET("/KitchenSink/chart", () => WrapPage<ChartPage>("/KitchenSink/partial/chart"));

            Handle.GET("/KitchenSink/partial/checkbox", () => new CheckboxPage());
            Handle.GET("/KitchenSink/checkbox", () => WrapPage<CheckboxPage>("/KitchenSink/partial/checkbox"));

            Handle.GET("/KitchenSink/partial/togglebutton", () => new ToggleButtonPage());
            Handle.GET("/KitchenSink/togglebutton",
                () => WrapPage<ToggleButtonPage>("/KitchenSink/partial/togglebutton"));

            Handle.GET("/KitchenSink/partial/datagrid", () => new DatagridPage());
            Handle.GET("/KitchenSink/datagrid", () => WrapPage<DatagridPage>("/KitchenSink/partial/datagrid"));

            Handle.GET("/KitchenSink/partial/decimal", () => new DecimalPage());
            Handle.GET("/KitchenSink/decimal", () => WrapPage<DecimalPage>("/KitchenSink/partial/decimal"));

            Handle.GET("/KitchenSink/partial/dropdown", () => new DropdownPage());
            Handle.GET("/KitchenSink/dropdown", () => WrapPage<DropdownPage>("/KitchenSink/partial/dropdown"));

            Handle.GET("/KitchenSink/partial/html", () => new HtmlPage());
            Handle.GET("/KitchenSink/html", () => WrapPage<HtmlPage>("/KitchenSink/partial/html"));

            Handle.GET("/KitchenSink/partial/integer", () => new IntegerPage());
            Handle.GET("/KitchenSink/integer", () => WrapPage<IntegerPage>("/KitchenSink/partial/integer"));

            Handle.GET("/KitchenSink/partial/Geo", () =>
            {
                return Db.Scope(() =>
                {
                    var geoPage = new GeoPage();
                    geoPage.Init();
                    return geoPage;
                });
            });
            Handle.GET("/KitchenSink/Geo", () => WrapPage<GeoPage>("/KitchenSink/partial/Geo"));

            Handle.GET("/KitchenSink/partial/markdown", () => new MarkdownPage());
            Handle.GET("/KitchenSink/markdown", () => WrapPage<MarkdownPage>("/KitchenSink/partial/markdown"));

            Handle.GET("/KitchenSink/partial/nested", () => new NestedPartial
            {
                Data = new AnyData()
            });
            Handle.GET("/KitchenSink/nested", () => WrapPage<NestedPartial>("/KitchenSink/partial/nested"));

            Handle.GET("/KitchenSink/partial/radiolist", () => new RadiolistPage());
            Handle.GET("/KitchenSink/radiolist", () => WrapPage<RadiolistPage>("/KitchenSink/partial/radiolist"));

            Handle.GET("/KitchenSink/partial/multiselect", () => new MultiselectPage());
            Handle.GET("/KitchenSink/multiselect", () => WrapPage<MultiselectPage>("/KitchenSink/partial/multiselect"));

            Handle.GET("/KitchenSink/partial/password", () => new PasswordPage());
            Handle.GET("/KitchenSink/password", () => WrapPage<PasswordPage>("/KitchenSink/partial/password"));

            Handle.GET("/KitchenSink/partial/table", () => new TablePage());
            Handle.GET("/KitchenSink/table", () => WrapPage<TablePage>("/KitchenSink/partial/table"));

            Handle.GET("/KitchenSink/partial/text", () => new TextPage());
            Handle.GET("/KitchenSink/text", () => WrapPage<TextPage>("/KitchenSink/partial/text"));

            Handle.GET("/KitchenSink/partial/textarea", () => new TextareaPage());
            Handle.GET("/KitchenSink/textarea", () => WrapPage<TextareaPage>("/KitchenSink/partial/textarea"));

            Handle.GET("/KitchenSink/partial/radio", () => new RadioPage());
            Handle.GET("/KitchenSink/radio", () => WrapPage<RadioPage>("/KitchenSink/partial/radio"));

            Handle.GET("/KitchenSink/partial/Redirect", () => new RedirectPage());
            Handle.GET("/KitchenSink/Redirect", () => WrapPage<RedirectPage>("/KitchenSink/partial/Redirect"));

            Handle.GET("/KitchenSink/partial/Validation", () => new ValidationPage());
            Handle.GET("/KitchenSink/Validation", () => WrapPage<ValidationPage>("/KitchenSink/partial/Validation"));

            Handle.GET("/KitchenSink/Redirect/{?}", (string param) =>
            {
                var master = WrapPage<RedirectPage>("/KitchenSink/partial/Redirect") as MasterPage;
                var nav = master.CurrentPage as NavPage;
                var page = nav.CurrentPage as RedirectPage;
                page.YourFavoriteFood = "You've got some tasty " + param;
                return master;
            });

            Handle.GET("/KitchenSink/partial/url", () => new UrlPage());
            Handle.GET("/KitchenSink/url", () => WrapPage<UrlPage>("/KitchenSink/partial/url"));

            Handle.GET("/KitchenSink/partial/datepicker", () => new DatepickerPage());
            Handle.GET("/KitchenSink/datepicker", () => WrapPage<DatepickerPage>("/KitchenSink/partial/datepicker"));

            Handle.GET("/KitchenSink/partial/fileupload", () => new FileUploadPage());
            Handle.GET("/KitchenSink/fileupload", () => WrapPage<FileUploadPage>("/KitchenSink/partial/fileupload"));

            Handle.GET("/KitchenSink/partial/callback", () => new CallbackPage());
            Handle.GET("/KitchenSink/callback", () => WrapPage<CallbackPage>("/KitchenSink/partial/callback"));

            Handle.GET("/KitchenSink/partial/dialog", () => new DialogPage());
            Handle.GET("/KitchenSink/dialog", () => WrapPage<DialogPage>("/KitchenSink/partial/dialog"));

            Handle.GET("/KitchenSink/partial/progressbar", () => new ProgressBarPage());
            Handle.GET("/Kitchensink/progressbar", () => WrapPage<ProgressBarPage>("/KitchenSink/partial/progressbar"));

            Handle.GET("/KitchenSink/partial/lazyloading", () => new LazyLoadingPage());
            Handle.GET("/Kitchensink/lazyloading", () => WrapPage<LazyLoadingPage>("/KitchenSink/partial/lazyloading"));

            Handle.GET("/KitchenSink/partial/pagination", () => new PaginationPage());
            Handle.GET("/Kitchensink/pagination", () => WrapPage<PaginationPage>("/KitchenSink/partial/pagination"));

            Handle.GET("/KitchenSink/partial/flashmessage", () => new FlashMessagePage());
            Handle.GET("/Kitchensink/flashmessage", () => WrapPage<FlashMessagePage>("/KitchenSink/partial/flashmessage"));

            Handle.GET("/KitchenSink/partial/sortablelist", () => new SortableListPage());
            Handle.GET("/Kitchensink/sortablelist", () => WrapPage<SortableListPage>("/KitchenSink/partial/sortablelist"));

            Handle.GET("/KitchenSink/partial/cookie", (Request request) =>
            {
                string name = "KitchenSink";
                CookiePage page = new CookiePage();
                Cookie cookie = request.Cookies.Select(x => new Cookie(x)).FirstOrDefault(x => x.Name == name);

                if (cookie != null)
                {
                    page.RequestCookie = cookie.Value;
                }

                cookie = new Cookie()
                {
                    Name = name,
                    Value = string.Format("KitchenSinkCookie-{0}", DateTime.Now.ToString()),
                    Expires = DateTime.Now.AddDays(1)
                };

                Handle.AddOutgoingCookie(name, cookie.GetFullValueString());

                return page;
            });
            Handle.GET("/KitchenSink/cookie", () => WrapPage<CookiePage>("/KitchenSink/partial/cookie"));

            HandleFile.GET("/KitchenSink/fileupload/upload", task =>
            {
                Session.ScheduleTask(task.SessionId, (s, id) =>
                {
                    MasterPage master = s.Data as MasterPage;

                    if (master == null)
                    {
                        return;
                    }

                    NavPage nav = master.CurrentPage as NavPage;

                    if (nav == null)
                    {
                        return;
                    }

                    FileUploadPage page = nav.CurrentPage as FileUploadPage;

                    if (page == null)
                    {
                        return;
                    }

                    var item = page.Tasks.FirstOrDefault(x => x.FileName == task.FileName);

                    if (task.State == HandleFile.UploadTaskState.Error)
                    {
                        if (item != null)
                        {
                            page.Tasks.Remove(item);
                        }
                    }
                    else if (task.State == HandleFile.UploadTaskState.Completed)
                    {
                        if (item != null)
                        {
                            page.Tasks.Remove(item);
                        }

                        var file = page.Files.FirstOrDefault(x => x.FileName == task.FileName);

                        if (file == null)
                        {
                            file = page.Files.Add();
                        }

                        file.FileName = task.FileName;
                        file.FileSize = task.FileSize;
                        file.FilePath = task.FilePath;
                    }
                    else
                    {
                        if (item == null)
                        {
                            item = page.Tasks.Add();
                        }

                        item.FileName = task.FileName;
                        item.FileSize = task.FileSize;
                        item.Progress = task.Progress;
                    }

                    s.CalculatePatchAndPushOnWebSocket();
                });
            });

            Handle.GET("/KitchenSink/partial/autocomplete", () => Db.Scope(() => new AutocompletePage()));
            Handle.GET("/KitchenSink/autocomplete", () => WrapPage<AutocompletePage>("/KitchenSink/partial/autocomplete"));

            //for a launcher
            Handle.GET("/KitchenSink/app-name", () => { return new AppName(); });

            Handle.GET("/KitchenSink/menu", () => { return new AppMenuPage(); });

            UriMapping.Map("/KitchenSink/menu", UriMapping.MappingUriPrefix + "/menu");
            UriMapping.Map("/KitchenSink/app-name", UriMapping.MappingUriPrefix + "/app-name");
        }

        private static Json WrapPage<T>(string partialPath) where T : Json
        {
            var master = (MasterPage) Self.GET("/KitchenSink/master");
            var nav = master.CurrentPage as NavPage;

            if (nav.CurrentPage != null && nav.CurrentPage.GetType().Equals(typeof(T)))
            {
                return master;
            }

            nav.CurrentPage = Self.GET(partialPath);

            if (nav.CurrentPage.Data == null)
            {
                nav.CurrentPage.Data = null; //trick to invoke OnData in partial
            }

            return master;
        }

        private static int IndexOfNth(string str, char c, int n)
        {
            int index = -1;
            while (n-- > 0)
            {
                index = str.IndexOf(c, index + 1);
                if (index == -1) break;
            }
            return index;
        }
    }
}