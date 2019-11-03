using System;
using Shouldly;
using Xunit;

namespace Sharp7.Tests
{
    public class ClientTest : ServerClientTestBase
    {

        [Fact]
        public void ClientIsNotNull()
        {
            Client.ShouldNotBeNull();
        }

        [Fact]
        public void ServerIsNotNull()
        {
            Server.ShouldNotBeNull();
        }

        [Fact]
        public void Timeout()
        {
            Client.ConnTimeout.ShouldBe(2000);
        }

        [Fact]
        public void Connected()
        {
            Client.Connected.ShouldBe(true);
        }

        [Fact]
        public void Port()
        {
            Client.PLCPort.ShouldBe(102);
        }

        [Fact]
        public void ReadWriteDb()
        {
            var bytes = new byte[] { 1, 2, 3 };
            var index = 3;
            Server.RegisterArea(S7Server.SrvAreaDB, index, ref bytes, bytes.Length);

            var buffer = new byte[bytes.Length];
            var rc = Client.DBRead(index, 0, bytes.Length, buffer);

            //test read
            rc.ShouldBe(0);
            buffer.ShouldBe(bytes);

            buffer = new byte[] { 3, 2, 1 };
            rc = Client.DBWrite(index, 0, bytes.Length, buffer);

            //test write
            rc.ShouldBe(0);
            bytes.ShouldBe(buffer);
        }

        [Fact]
        public void ReadWriteAb()
        {
            var bytes = new byte[] { 1, 2, 3 };
            Server.RegisterArea(S7Server.SrvAreaPa, 0, ref bytes, bytes.Length);

            var buffer = new byte[bytes.Length];
            var rc = Client.ABRead(0, bytes.Length, buffer);

            //test read
            rc.ShouldBe(0);
            buffer.ShouldBe(bytes);

            buffer = new byte[] { 3, 2, 1 };
            rc = Client.ABWrite(0, bytes.Length, buffer);

            //test write
            rc.ShouldBe(0);
            bytes.ShouldBe(buffer);
        }

        [Fact]
        public void ReadWriteCt()
        {
            var bytes = new byte[] { 0,1,2,3,4,5,6,7,8};
            var index = 3;
            Server.RegisterArea(S7Server.SrvAreaCt, index, ref bytes, bytes.Length/2);

            var buffer = new ushort[2];
            var rc = Client.CTRead(0, 2, buffer);

            //test read
            rc.ShouldBe(0);
            buffer.ShouldBe(new ushort[]{0x0100,0x0302});

            buffer = new ushort[] {0x0403, 0x0201 };
            rc = Client.CTWrite(0, 2, buffer);

            //test write
            rc.ShouldBe(0);
            bytes.ShouldBe(new byte[] { 3, 4, 1, 2, 4, 5, 6, 7, 8 });
        }

        [Fact]
        public void ReadWriteMb()
        {
            var bytes = new byte[] { 0, 1, 2};
            Server.RegisterArea(S7Server.SrvAreaMk, 0, ref bytes, bytes.Length);

            var buffer = new byte[bytes.Length];
            var rc = Client.MBRead(0,bytes.Length,buffer);

            //test read
            rc.ShouldBe(0);
            buffer.ShouldBe(bytes);

            buffer = new byte[] { 3, 2, 1 };
            rc = Client.MBWrite(0, bytes.Length, buffer);

            //test write
            rc.ShouldBe(0);
            bytes.ShouldBe(buffer);
        }

        [Fact]
        public void ReadWriteTm()
        {
            var bytes = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
            var index = 5;
            Server.RegisterArea(S7Server.SrvAreaTm, index, ref bytes, bytes.Length / 2);

            var buffer = new ushort[2];
            var rc = Client.TMRead(0, 2, buffer);

            //test read
            rc.ShouldBe(0);
            buffer.ShouldBe(new ushort[] { 0x0100, 0x0302 });

            buffer = new ushort[] { 0x0403, 0x0201 };
            rc = Client.TMWrite(0, 2, buffer);

            //test write
            rc.ShouldBe(0);
            bytes.ShouldBe(new byte[] {3, 4, 1, 2, 4, 5, 6, 7, 8, 9, 10, 11});
        }

        [Fact]
        public void ReadWriteEb()
        {
            var bytes = new byte[] { 0, 1, 2 };
            Server.RegisterArea(S7Server.SrvAreaPe, 0, ref bytes, bytes.Length);

            var buffer = new byte[bytes.Length];
            var rc = Client.EBRead(0, bytes.Length, buffer);

            //test read
            rc.ShouldBe(0);
            buffer.ShouldBe(bytes);

            buffer = new byte[] { 3, 2, 1 };
            rc = Client.EBWrite(0, bytes.Length, buffer);

            //test write
            rc.ShouldBe(0);
            bytes.ShouldBe(buffer);
        }
    }
}
