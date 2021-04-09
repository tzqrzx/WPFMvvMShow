using System.Threading.Tasks;

namespace Consumption.ViewModel.Interfaces
{
    public interface IBaseCenter
    {
        /// <summary>
        /// 关联默认数据上下文
        /// </summary>
        void BindDefaultModel();

        object GetView();

        /// <summary>
        /// 关联默认数据上下文(包含权限相关)
        /// </summary>
        Task BindDefaultModel(int AuthValue = 0);


        /// <summary>
        /// 关联表格列
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        void BindDataGridColumns();
    }
}
