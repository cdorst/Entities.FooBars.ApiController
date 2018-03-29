// Copyright © Christopher Dorst. All rights reserved.
// Licensed under the GNU General Public License, Version 3.0. See the LICENSE document in the repository root for license information.

using DevOps.Code.DataAccess.Interfaces.Repository;
using Entities.FooBars.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Entities.FooBars.ApiController
{
    /// <summary>ASP.NET Core web API controller for FooBar entities</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FooBarsController : ControllerBase
    {
        /// <summary>Represents the application events logger</summary>
        private readonly ILogger<FooBarsController> _logger;

        /// <summary>Represents repository of FooBar entity data</summary>
        private readonly IRepository<FooBarDbContext, FooBar, > _repository;

        /// <summary>Constructs an API controller for FooBar entities using the given repository service</summary>
        public FooBarsController(ILogger<FooBarsController> logger, IRepository<FooBarDbContext, FooBar, > repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>Handles HTTP DELETE requests to remove FooBar resources at the given ID</summary>
        [HttpDelete("{id}")]
        public async Task Delete(id)
        {
            await _repository.RemoveAsync(id);
            return Ok();
        }

        /// <summary>Handles HTTP GET requests to access FooBar resources at the given ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<FooBar>> Get(id)
        {var resource = await _repository.FindAsync(id);
        if (resource == null) return NotFound();
        return resource;
        }

        /// <summary>Handles HTTP HEAD requests to access FooBar resources at the given ID</summary>
        [HttpHead("{id}")]
        public ActionResult<FooBar> Head(id) => null;

        /// <summary>Handles HTTP PATCH requests to modify FooBar resources at the given ID</summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<FooBar>> Patch(id, JsonPatchDocument<FooBar> patch)
        {var resource = await _repository.FindAsync(id);
        if (resource == null) return NotFound();
        patch.ApplyTo(resource, ModelState);
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return await _repository.UpdateAsync(resource);
        }

        /// <summary>Handles HTTP POST requests to save FooBar resources</summary>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<FooBar>> Post(FooBar resource)
        {
            var saved = await _repository.AddAsync(resource);
            return CreatedAtAction(nameof(Get), new { id = saved.GetKey() }, saved);
        }

        /// <summary>Handles HTTP PUT requests to add or update FooBar resources</summary>
        [HttpPut]
        public async Task<ActionResult<FooBar>> Put(FooBar resource)
        {
            var id = entity.GetKey();
            if (id == 0) resource = await _repository.AddAsync(resource);else resource = await _repository.UpdateAsync(resource);return resource;
        }
    }
}