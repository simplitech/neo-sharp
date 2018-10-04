﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeoSharp.Core.Blockchain.Genesis;
using NeoSharp.Core.Blockchain.Repositories;
using NeoSharp.Core.Extensions;
using NeoSharp.Core.Logging;
using NeoSharp.Core.Models;
using NeoSharp.Core.Models.OperationManger;
using NeoSharp.Core.Network;

namespace NeoSharp.Core.Blockchain.Processing
{
    /// <inheritdoc />
    public class BlockHeaderPersister : IBlockHeaderPersister
    {
        #region Private Fields
        private readonly IBlockRepository _blockRepository;
        private readonly ISigner<BlockHeader> _blockHeaderSigner;
        private readonly IBlockchainContext _blockchainContext;
        private readonly IGenesisBuilder _genesisBuilder;
        private readonly ILogger<BlockHeaderPersister> _logger;
        #endregion        

        #region Constructor 
        public BlockHeaderPersister(
            IBlockRepository blockRepository,
            ISigner<BlockHeader> blockHeaderSigner,
            IBlockchainContext blockchainContext,
            IGenesisBuilder genesisBuilder, 
            ILogger<BlockHeaderPersister> logger)
        {
            _blockRepository = blockRepository;
            _blockHeaderSigner = blockHeaderSigner;
            _blockchainContext = blockchainContext;
            _genesisBuilder = genesisBuilder;
            _logger = logger;
        }
        #endregion

        #region IBlockHeaderPersister implementation 
        public async Task Update(BlockHeader blockHeader)
		{
			if(blockHeader == null) throw new ArgumentNullException(nameof(blockHeader));

			await _blockRepository.UpdateBlockHeader(blockHeader);
		}

		public async Task<IEnumerable<BlockHeader>> Persist(params BlockHeader[] blockHeaders)
        {
            if (blockHeaders == null) throw new ArgumentNullException(nameof(blockHeaders));

            var blockHeadersToPersist = _blockchainContext.LastBlockHeader == null ?
                blockHeaders
                    .ToList() :         // Persisting the Genesis block
                blockHeaders
                    .Where(bh => bh != null && bh.Index > _blockchainContext.LastBlockHeader.Index)
                    .Distinct(bh => bh.Index)
                    .OrderBy(bh => bh.Index)
                    .ToList();

            foreach (var blockHeader in blockHeadersToPersist)
            {
                _blockHeaderSigner.Sign(blockHeader);

                if (!IsBlockHeaderValid(blockHeader))
                {
                    _logger.LogInformation($"Block header with hash {blockHeader.Hash} and index {blockHeader.Index} is invalid and will not be persist.");
                    blockHeadersToPersist.Remove(blockHeader);
                    break;
                }

                await _blockRepository.AddBlockHeader(blockHeader);                
                _blockchainContext.LastBlockHeader = blockHeader;
            }

            return blockHeadersToPersist;
        }
        #endregion

        #region Private methods
        private bool IsBlockHeaderValid(BlockHeader blockHeader)
        {
            if (_blockchainContext.LastBlockHeader != null)
            {
                if (_blockchainContext.LastBlockHeader.Index + 1 != blockHeader.Index ||
                    _blockchainContext.LastBlockHeader.Hash != blockHeader.PreviousBlockHash)
                {
                    return false;
                }
            }
            else
            {
                if (blockHeader.Index != 0 || blockHeader.Hash != _genesisBuilder.Build().Hash)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}