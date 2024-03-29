namespace NotePro
{
    public class zh_CN : L
    {
        public override string Notes => "笔记列表";

        public override string EditNote => "编辑笔记";
        public override string AddNote  => "创建笔记";

        public override string Low      => "低";
        public override string High     => "高";
        public override string VeryHigh => "非常高";

        public override string DiscardChanges => "丢弃修改 ?";

        public override string DiscardChangesContent => "确定要丢弃修改 ?";
        public override string Yes                   => "是";
        public override string No                    => "否";


        public override string DeleteNote        => "删除笔记 ?";
        public override string DeleteNoteContent => "确定要删除笔记 ?";
        public override string Title             => "标题";
        public override string Description       => "描述";


        #region 过滤

        public override string Inbox => "收件箱";
        public override string All   => "全部笔记";

        public override string Priority => "优先级";

        public override string Color => "颜色";

        public override string Notebook => "笔记本";

        #endregion


        #region 笔记本

        public override string CreateNotebook => "创建笔记本";


        public override string EditNotebook => "编辑笔记本";

        public override string NameOfNotebook => "笔记本名字";

        public override string Delete => "删除";

        public override string Create => "创建";

        public override string Save => "保存";

        public override string Cancel => "取消";

        public override string SelectNotebook => "请选择笔记本";

        public override string MarkdownPreview => "Markdown 预览";

        public override string NoneSelect => "不选择";

        #endregion
    }
}