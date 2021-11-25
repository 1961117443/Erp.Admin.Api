using FreeSql;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Yitter.IdGenerator;

namespace Furion.Extras.Admin.NET
{
    /// <summary>
    /// 自定义实体基类
    /// </summary>
    public abstract class DEntityBase : DEntityBase<long>
    {
        protected override void OnCreate()
        {
            Id = YitIdHelper.NextId();
            base.OnCreate();
        }

        /// <summary>
        /// 更新信息列
        /// </summary>
        /// <returns></returns>
        public virtual string[] UpdateColumn()
        {
            var result = new[] { nameof(UpdatedUserId), nameof(UpdatedUserName), nameof(UpdatedTime) };
            return result;
        }

        /// <summary>
        /// 假删除的列，包含更新信息
        /// </summary>
        /// <returns></returns>
        protected virtual string[] FalseDeleteColumn()
        {
            var updateColumn = UpdateColumn();
            var deleteColumn = new[] { nameof(IsDeleted) };
            var result = new string[updateColumn.Length + deleteColumn.Length];
            deleteColumn.CopyTo(result, 0);
            updateColumn.CopyTo(result, deleteColumn.Length);
            return result;
        }
    }

    public abstract class DEntityBase<TKey, TDbContextLocator1> : PrivateDEntityBase<TKey>
        where TDbContextLocator1 : class, IDbContextLocator
    {
    }
    public abstract class DEntityBase<TKey> : PrivateDEntityBase<TKey>
    {
        protected virtual void OnCreate()
        {

        }
        public virtual void Create()
        {
            OnCreate();
            var userId = App.User.FindFirst(ClaimConst.CLAINM_USERID)?.Value;
            var userName = App.User.FindFirst(ClaimConst.CLAINM_ACCOUNT)?.Value;
            //Id = YitIdHelper.NextId();
            CreatedTime = DateTime.Now;
            if (!string.IsNullOrEmpty(userId))
            {
                CreatedUserId = long.Parse(userId);
                CreatedUserName = userName;
            }
        }

        public virtual void Modify()
        {
            var userId = App.User.FindFirst(ClaimConst.CLAINM_USERID)?.Value;
            var userName = App.User.FindFirst(ClaimConst.CLAINM_ACCOUNT)?.Value;
            UpdatedTime = DateTime.Now;
            if (!string.IsNullOrEmpty(userId))
            {
                UpdatedUserId = long.Parse(userId);
                UpdatedUserName = userName;
            }
        }
    }
    public abstract class PrivateDEntityBase<TKey> : IPrivateEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Comment("Id主键")]
        public virtual TKey Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Comment("创建时间")]
        public virtual DateTime? CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Comment("更新时间")]
        public virtual DateTime? UpdatedTime { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        [Comment("创建者Id")]
        public virtual long? CreatedUserId { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        [Comment("创建者名称")]
        [MaxLength(20)]
        public virtual string CreatedUserName { get; set; }

        /// <summary>
        /// 修改者Id
        /// </summary>
        [Comment("修改者Id")]
        public virtual long? UpdatedUserId { get; set; }

        /// <summary>
        /// 修改者名称
        /// </summary>
        [Comment("修改者名称")]
        [MaxLength(20)]
        public virtual string UpdatedUserName { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        [JsonIgnore]
        [Comment("软删除标记")]
        public virtual bool IsDeleted { get; set; } = false;
    }

    /// <summary>
    /// 递增主键实体基类
    /// </summary>
    public abstract class AutoIncrementEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [FreeSql.DataAnnotations.Column(IsIdentity = true, /*ColumnDescription = "Id主键",*/ IsPrimary = true)] //通过特性设置主键和自增列 
        // 注意是在这里定义你的公共实体        
        public virtual int Id { get; set; }
    }

    /// <summary>
    /// 主键实体基类
    /// </summary>
    public abstract class PrimaryKeyEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [FreeSql.DataAnnotations.Column(/*ColumnDescription = "Id主键", */IsPrimary = true)]
        // 注意是在这里定义你的公共实体
        public virtual long Id { get; set; }
    }
}