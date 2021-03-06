using System;
using System.Collections.Generic;
using System.Linq;
using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public class MemoryManager : IMemoryManager
    {
        enum AddressState
        {
            Free,
            NotImplemented,
            Reserved,
            Allocated,

        }
        private readonly AddressState[] _memoryMap;
        private readonly Dictionary<IValueDestination, ResultLocation> _locations = new Dictionary<IValueDestination, ResultLocation>();

        public MemoryManager(int totalSize)
        {
            _memoryMap = new AddressState[totalSize];
            Set(0, totalSize, AddressState.Free);
        }

        public void SetNotImplemented(int address)
        {
            Set(address, 1, AddressState.NotImplemented);
        }

        public void SetNotImplemented(int address, int size)
        {
            Set(address, size, AddressState.NotImplemented);
        }

        public void SetFree(int address, int size)
        {
            Set(address, size, AddressState.Free);
        }

        public void SetReserved(int address, int size)
        {
            Set(address, size, AddressState.Reserved);
        }

        public void SetAllocated(int address, int size)
        {
            Set(address, size, AddressState.Allocated);
        }

        public ResultLocation Alloc(int size)
        {
            if (size <= 0)
                throw new ArgumentException("size should be greater than 0.");

            var location = FindFreeBlock(size);
            Set(location, size, AddressState.Allocated);
            return new ResultLocation(location);
        }

        private int FindFreeBlock(int size)
        {
            var result = TryFindFreeBlock(size);
            if (result < 0)
                throw new InvalidOperationException("Out of memory.");
            return result;
        }

        private int TryFindFreeBlock(int size)
        {
            for (int i = 0; i < _memoryMap.Length - size; i++)
            {
                if (AllIs(i, size, AddressState.Free))
                {
                    return i;
                }
            }
            return -1;
        }

        private void Set(int address, int size, AddressState state)
        {
            if (address < 0)
                throw new ArgumentException("address should be greater or equal to 0.");

            if (size <= 0)
                throw new ArgumentException("size should be greater than 0.");

            if (address + size > _memoryMap.Length)
                throw new ArgumentException("address + size should be less then total memory size.");

            for (int i = address; i < address + size; i++)
            {
                _memoryMap[i] = state;
            }
        }

        private bool AllIs(int address, int size, AddressState state)
        {
            if (address < 0)
                throw new ArgumentException("address should be greater or equal to 0.");

            if (size <= 0)
                throw new ArgumentException("size should be greater than 0.");

            if (address + size >= _memoryMap.Length)
                throw new ArgumentException("address + size should be less then total memory size.");

            for (int i = address; i < address + size; i++)
            {
                if (_memoryMap[i] != state) return false;
            }
            return true;
        }

        public void Dispose(ResultLocation location, int size)
        {
            if (location.IsWorkRegister)
            {
                if (size == 1) return;
                throw new InvalidOperationException("Working register should only containe single byte of data.");
            }

            var address = location.Address;

            if (!AllIs(address, size, AddressState.Allocated))
            {
                throw new InvalidOperationException("This memory wasn't allocated.");
            }

            Set(location.Address, size, AddressState.Free);
        }

        public ResultLocation Alloc(IValueDestination destination)
        {
            var location = destination.Location;
            if (location == null && !_locations.TryGetValue(destination, out location))
            {
                var address = FindFreeBlock(destination.Type.Size);
                Set(address, destination.Type.Size, AddressState.Reserved);
                location = new ResultLocation(address);
                _locations.Add(destination, location);
            }
            return location;
        }

        public void DAlloc(IValueDestination destination)
        {
            throw new NotImplementedException();
        }

        public int CalcAvailableBytes()
        {
            return _memoryMap.Where(x => x == AddressState.Free).Count();
        }
    }
}