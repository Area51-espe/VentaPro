using System;
using System.Windows.Controls;

namespace VentaPro.View
{
    /// <summary>
    /// Lógica de interacción para VentasView.xaml
    /// </summary>
    public partial class VentasView : UserControl
    {
        public VentasView()
        {
            InitializeComponent();
        }
        private bool _isCommittingRowEdit;

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit && !_isCommittingRowEdit)
            {
                _isCommittingRowEdit = true;
                DataGrid dataGrid = sender as DataGrid;
                if (dataGrid != null)
                {
                    dataGrid.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                        _isCommittingRowEdit = false;
                    }));
                }
            }
        }



    }
}
