using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Workspace;
using ComponentFactory.Krypton.Docking;
using JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid;

namespace WindowsFormsApp1
{
    public partial class ProductsListForm : KryptonForm
    {
        private int _count = 1;

        public ProductsListForm()
        {
            InitializeComponent();
        }

        private KryptonPage NewDocument()
        {
            KryptonPage page = NewPage("Document ", 0, kryptonOutlookGrid1);

            // Document pages cannot be docked or auto hidden
            page.ClearFlags(KryptonPageFlags.DockingAllowAutoHidden | KryptonPageFlags.DockingAllowDocked | KryptonPageFlags.DockingAllowClose | KryptonPageFlags.All);
            return page;
        }

        private void workspaceCell_CloseAction(object sender, CloseActionEventArgs e)
        {
            e.Action = CloseButtonAction.None;
        }

        private KryptonPage NewInput()
        {
            return NewPage("Input ", 1, new KryptonOutlookGrid());
        }

        private KryptonPage NewPropertyGrid()
        {
            return NewPage("Properties ", 2, new KryptonOutlookGrid());
        }

        private KryptonPage NewPage(string name, int image, Control content)
        {
            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = name + _count.ToString();
            p.TextTitle = name + _count.ToString();
            p.TextDescription = name + _count.ToString();


            // Add the control for display inside the page
            content.Dock = DockStyle.Fill;
            p.Controls.Add(content);

            _count++;
            return p;
        }

        private void ProductsListForm_Load(object sender, EventArgs e)
        {
            // Setup docking functionality
            KryptonDockingWorkspace w = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl(kryptonPanel, w);
            kryptonDockingManager.ManageFloating(this);

            // Add initial docking pages
            kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { NewDocument() });
            KryptonWorkspaceCell workspaceCell = kryptonDockingManager.CellsWorkspace.FirstOrDefault();
            workspaceCell.CloseAction += workspaceCell_CloseAction;
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Left, new KryptonPage[] { NewInput(), NewInput() });
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Bottom, new KryptonPage[] { NewPropertyGrid() });

            // Set correct initial ribbon palette buttons
        }

        private void buttonDocumentSingle_Click(object sender, EventArgs e)
        {
            // Get access to current active cell or create new cell if none are present
            KryptonWorkspaceCell cell = kryptonDockableWorkspace.ActiveCell;
            if (cell == null)
            {
                cell = new KryptonWorkspaceCell();
                kryptonDockableWorkspace.Root.Children.Add(cell);
            }

            // Create new document to be added into workspace
            KryptonPage page = NewDocument();
            cell.Pages.Add(page);

            // Make the new page the selected page
            cell.SelectedPage = page;
        }

        private void buttonDocumentTabbed_Click(object sender, EventArgs e)
        {
            // Add a new cell with three pages into the root sequence of the workspace
            KryptonWorkspaceCell cell = new KryptonWorkspaceCell();
            cell.Pages.AddRange(new KryptonPage[] { NewDocument(), NewDocument(), NewDocument() });
            kryptonDockableWorkspace.Root.Children.Add(cell);
        }

        private void buttonFloatingSingle_Click(object sender, EventArgs e)
        {
            // Add a new floating window with a single page
            kryptonDockingManager.AddFloatingWindow("Floating", new KryptonPage[] { NewInput() });
        }

        private void buttonFloatingTabbed_Click(object sender, EventArgs e)
        {
            // Add a new floating window with two pages
            kryptonDockingManager.AddFloatingWindow("Floating", new KryptonPage[] { NewPropertyGrid(), NewDocument() });
        }

        private void buttonFloatingComplex_Click(object sender, EventArgs e)
        {
            // Add single page to a new floating window
            KryptonDockingFloatingWindow window = kryptonDockingManager.AddFloatingWindow("Floating",
                                                                                          new KryptonPage[] { NewInput() },
                                                                                          new Size(500, 400));

            // Create a sequence that contains two cells, with a page in each cell
            KryptonWorkspaceSequence seq = new KryptonWorkspaceSequence(Orientation.Vertical);
            KryptonWorkspaceCell cell1 = new KryptonWorkspaceCell();
            KryptonWorkspaceCell cell2 = new KryptonWorkspaceCell();
            seq.Children.AddRange(new Component[] { cell1, cell2 });
            cell1.Pages.Add(NewPropertyGrid());
            cell2.Pages.Add(NewDocument());

            // Add new sequence into the floating window
            window.FloatspaceElement.FloatspaceControl.Root.Children.Add(seq);
        }

        private void buttonLeftAutoHidden_Click(object sender, EventArgs e)
        {
            // Add new auto hidden group to left edge of the panel
            kryptonDockingManager.AddAutoHiddenGroup("Control",
                                                     DockingEdge.Left,
                                                     new KryptonPage[] { NewInput(), NewPropertyGrid() });
        }

        private void buttonRightAutoHidden_Click(object sender, EventArgs e)
        {
            // Add new auto hidden group to right edge of the panel
            kryptonDockingManager.AddAutoHiddenGroup("Control",
                                                     DockingEdge.Right,
                                                     new KryptonPage[] { NewInput(), NewPropertyGrid() });
        }

        private void buttonBottomAutoHidden_Click(object sender, EventArgs e)
        {
            // Add new auto hidden group to bottom edge of the panel
            kryptonDockingManager.AddAutoHiddenGroup("Control",
                                                     DockingEdge.Bottom,
                                                     new KryptonPage[] { NewInput(), NewPropertyGrid(), NewDocument() });
        }

        private void buttonLeftDockedSingle_Click(object sender, EventArgs e)
        {
            // Add page to left edge of the panel
            kryptonDockingManager.AddDockspace("Control",
                                               DockingEdge.Left,
                                               new KryptonPage[] { NewInput() });
        }

        private void buttonLeftDockedTabbed_Click(object sender, EventArgs e)
        {
            // Add three tabbed pages to left edge of the panel
            kryptonDockingManager.AddDockspace("Control",
                                               DockingEdge.Left,
                                               new KryptonPage[] { NewInput(), NewPropertyGrid(), NewDocument() });
        }

        private void buttonLeftDockedStack_Click(object sender, EventArgs e)
        {
            // Add three vertical-stacked pages to left edge of the panel
            kryptonDockingManager.AddDockspace("Control",
                                               DockingEdge.Left,
                                               new KryptonPage[] { NewDocument() },
                                               new KryptonPage[] { NewDocument() },
                                               new KryptonPage[] { NewDocument() });
        }

        private void buttonRightDockedSingle_Click(object sender, EventArgs e)
        {
            // Add page to right edge of the panel
            kryptonDockingManager.AddDockspace("Control",
                                               DockingEdge.Right,
                                               new KryptonPage[] { NewInput() });
        }

        private void buttonTopDockedTabbed_Click(object sender, EventArgs e)
        {
            // Add three tabbed pages to top edge of the panel
            kryptonDockingManager.AddDockspace("Control",
                                               DockingEdge.Top,
                                               new KryptonPage[] { NewInput(), NewPropertyGrid(), NewDocument() });
        }

        private void buttonBottomDockedStack_Click(object sender, EventArgs e)
        {
            // Add three horizontal-stacked pages to bottom edge of the panel
            kryptonDockingManager.AddDockspace("Control",
                                               DockingEdge.Bottom,
                                               new KryptonPage[] { NewDocument() },
                                               new KryptonPage[] { NewDocument() },
                                               new KryptonPage[] { NewDocument() });
        }

        private void buttonDeleteAll_Click(object sender, EventArgs e)
        {
            kryptonDockingManager.RemoveAllPages(true);
            kryptonDockingManager.ClearAllStoredPages();
        }

        private void buttonDeleteDocked_Click(object sender, EventArgs e)
        {
            KryptonPage[] pages = kryptonDockingManager.PagesDocked;
            kryptonDockingManager.RemovePages(pages, true);
            kryptonDockingManager.ClearStoredPages(pages);
        }

        private void buttonDeleteAutoHidden_Click(object sender, EventArgs e)
        {
            KryptonPage[] pages = kryptonDockingManager.PagesAutoHidden;
            kryptonDockingManager.RemovePages(pages, true);
            kryptonDockingManager.ClearStoredPages(pages);
        }

        private void buttonDeleteFloating_Click(object sender, EventArgs e)
        {
            KryptonPage[] pages = kryptonDockingManager.PagesFloating;
            kryptonDockingManager.RemovePages(pages, true);
            kryptonDockingManager.ClearStoredPages(pages);
        }

        private void buttonHideAll_Click(object sender, EventArgs e)
        {
            kryptonDockingManager.HideAllPages();
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            kryptonDockingManager.ShowAllPages();
            kryptonDockableWorkspace.ShowAllPages();
        }

        private void kryptonDockableWorkspace_WorkspaceCellAdding(object sender, WorkspaceCellEventArgs e)
        {
            e.Cell.Button.CloseButtonAction = CloseButtonAction.HidePage;
        }

        private void ribbonAppButtonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
