using System.Collections;
using System.Collections.Generic;

namespace ApiTemplApiProject.Core.Service
{
    /// <summary>
    /// 侧边栏菜单
    /// </summary>
    public class SideMenuOutput : ITreeNode
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 菜单打开类型
        /// </summary>
        public string OpenType { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Href { get; set; }
        
        /// <summary>
        /// 子节点
        /// </summary>
        public List<SideMenuOutput> Children { get; set; } = new List<SideMenuOutput>();

        public long GetId()
        {
            return Id;
        }

        public long GetPid()
        {
            return ParentId;
        }

        public void SetChildren(IList children)
        {
            Children = (List<SideMenuOutput>)children;
        }
    }
}
