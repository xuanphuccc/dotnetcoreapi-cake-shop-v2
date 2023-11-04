using AutoMapper;
using dotnetcoreapi.cake.shop.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.application
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> :
        BaseReadOnlyService<TEntity, TEntityDto>,
        IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        #region Fields
        protected readonly IBaseRepository<TEntity> _baseRepository;
        #endregion

        #region Constructors
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng cần thêm</param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> CreateEntityAsync(TEntityCreateDto entityCreateDto)
        {
            var newEntity = await MapCreateAsync(entityCreateDto);

            var createdEntity = await _baseRepository.CreateEntityAsync(newEntity);

            var createdEntityDto = _mapper.Map<TEntityDto>(createdEntity);

            return createdEntityDto;
        }

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entityId">ID của đối tượng cần cập nhật</param>
        /// <param name="entityUpdateDto">Đối tượng cần cập nhật</param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> UpdateEntityAsync(int entityId, TEntityUpdateDto entityUpdateDto)
        {
            var updateEntity = await MapUpdateAsync(entityId, entityUpdateDto);

            var updatedEntity = await _baseRepository.UpdateEntityAsync(updateEntity);

            var updatedEntityDto = _mapper.Map<TEntityDto>(updatedEntity);

            return updatedEntityDto;
        }

        /// <summary>
        /// Xoá một bản ghi
        /// </summary>
        /// <param name="entityId">ID của bản ghi cần xoá</param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> DeleteEntityAsync(int entityId)
        {
            var entity = await _baseReadOnlyRepository.GetEntityByIdAsync(entityId);

            // Thực hiện hành động trước khi xoá
            await BeforeDeleteAsync(entity);

            var deletedEntity = await _baseRepository.DeleteEntityAsync(entity);

            // Thực hiện hành động sau khi xoá
            await AfterDeleteAsync(deletedEntity);

            var deletedEntityDto = _mapper.Map<TEntityDto>(deletedEntity);

            return deletedEntityDto;
        }

        /// <summary>
        /// Map DTO sang entity để thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected abstract Task<TEntity> MapCreateAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Map DTO sang entity để cập nhật bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected abstract Task<TEntity> MapUpdateAsync(int entityId, TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Thực hiện hành động trước khi xoá
        /// </summary>
        /// <param name="deletedEntity">Đối tượng đã xoá</param>
        /// <returns></returns>
        protected virtual Task BeforeDeleteAsync(TEntity entity)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Thực hiện hành động sau khi xoá
        /// </summary>
        /// <param name="deletedEntity">Đối tượng đã xoá</param>
        /// <returns></returns>
        protected virtual Task AfterDeleteAsync(TEntity deletedEntity)
        {
            return Task.CompletedTask;
        }
    }
}
