using BLL.Banco;
using FirstFloor.ModernUI.Windows.Controls;
using MODEL.Banco;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMAViews.View.Banco
{
    /// <summary>
    /// Interaction logic for ContasPagar.xaml
    /// </summary>
    public partial class ContasPagar : ModernWindow
    {
        private ContasPagarBLL _contasPagarBLL = null;
        public ContasPagarBLL ContasPagarBLL
        {
            get
            {
                if (_contasPagarBLL == null)
                    _contasPagarBLL = new ContasPagarBLL();
                return _contasPagarBLL;
            }
        }
        public ContasPagar()
        {
            InitializeComponent();
        }
        private void NovoContasPagar(object sender, RoutedEventArgs e)
        {
            new ContasPagarCadastro().Show();
            this.Close();
        }
        private void Pesquisar(object sender, RoutedEventArgs e)
        {
            var model = new ContasPagarModel();
            if ((bool)cbAtivoSim.IsChecked)
                model.Status = "S";
            else
                model.Status = "N";

            model.DataEmissao = tbDataDe.DisplayDate;
            var data = tbDataAte.DisplayDate;

            gridPesquisa.ItemsSource = ContasPagarBLL.Pesquisar(model,data);
            UpdateRowColor();
        }
        private void UpdateRowColor()
        {
            for (int i = 0; i < gridPesquisa.Items.Count; ++i)
            {
                DataGridRow row = GetRow(gridPesquisa, i);

                if (row.Item is ContasPagarGridModel)
                {
                    ContasPagarGridModel registry = (ContasPagarGridModel)row.Item;

                    for (int j = 0; j < gridPesquisa.Columns.Count; ++j)
                    {
                        if (registry.DiasAtraso > 0)
                            GetCell(gridPesquisa, row, j).Background = Brushes.Yellow;    
                    }
                }
            }
        }
        private DataGridRow GetRow(DataGrid dataGrid, int index)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dataGrid.ScrollIntoView(dataGrid.Items[index]);
                dataGrid.UpdateLayout();
                row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }
        private DataGridCell GetCell(DataGrid dataGrid, DataGridRow rowContainer, int column)
        {
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                // try to get the cell but it may possibly be virtualized
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    // now try to bring into view and retreive the cell
                    dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }
        private T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                    child = GetVisualChild<T>(v);
                if (child != null)
                    break;
            }
            return child;
        }
    }
}
