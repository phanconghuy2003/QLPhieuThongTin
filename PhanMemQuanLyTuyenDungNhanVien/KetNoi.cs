using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Transform;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PhanMemQuanLyTuyenDungNhanVien
{
    public class KetNoi
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;

        public KetNoi()
        {
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("DoAn_NoSql");
            collection = database.GetCollection<BsonDocument>("tuyendung");
        }

        public int KiemTraKetNoi()
        {
            try
            {
                client.ListDatabases();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public List<UngVien> FillThongTinUngVienTheoMaTTD(string maVTTD)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("maVTTD", maVTTD);
            var results = collection.Find(filter);
            var danhSachUngVien = new List<UngVien>();

            foreach (var result in results.ToList())
            {

                if (result.Contains("danhSachUngVien"))
                {
                    foreach (var ungVienBson in result["danhSachUngVien"].AsBsonArray)
                    {
                        var cacKyNangArray = ungVienBson["cacKyNang"].IsBsonNull ? new string[0] : ungVienBson["cacKyNang"].AsBsonArray.Select(x => x.ToString()).ToArray();
                        var ungVien = new UngVien
                        {
                            soCCCD = ungVienBson["soCCCD"].AsString,
                            hoTen = ungVienBson["hoTen"].AsString,
                            ngaySinh = DateTime.Parse(ungVienBson["ngaySinh"].AsString),
                            diaChi = ungVienBson["diaChi"].AsString,
                            email = ungVienBson["email"].AsString,
                            soDienThoai = ungVienBson["soDienThoai"].AsString,
                            hocVan = ungVienBson["hocVan"].AsString,
                            chuyenNganh = ungVienBson["chuyenNganh"].AsString,
                            GPA = ungVienBson["GPA"].AsString,
                            cacKyNang = cacKyNangArray.ToList(),
                            chiTietKyNang = ungVienBson["chiTietKyNang"].AsString,
                            kinhNghiemLamViec = ungVienBson["kinhNghiemLamViec"].AsString,
                            cacDuAn = ungVienBson["cacDuAn"].AsString,
                            mucTieuCaNhan = ungVienBson["mucTieuCaNhan"].AsString
                        };

                        danhSachUngVien.Add(ungVien);
                    }
                }
            }
            return danhSachUngVien;
        }

        public List<string> LayDanhSachTenVTTD()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var projection = Builders<BsonDocument>.Projection.Include("tenVTTD").Exclude("_id");

            var result = collection.Find(filter).Project(projection).ToList();
            var tenVTTDs = new List<string>();

            foreach (var document in result)
            {
                var tenVTTD = document["tenVTTD"].AsString;
                tenVTTDs.Add(tenVTTD);
            }

            return tenVTTDs;
        }

        public List<UngVien> FillThongTinUngVienTheoTenTTD(string tenVTTD)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("tenVTTD", tenVTTD);
            var results = collection.Find(filter);
            var danhSachUngVien = new List<UngVien>();

            foreach (var result in results.ToList())
            {
                if (result.Contains("danhSachUngVien"))
                {
                    foreach (var ungVienBson in result["danhSachUngVien"].AsBsonArray)
                    {
                        var cacKyNangArray = ungVienBson["cacKyNang"].IsBsonNull ? new string[0] : ungVienBson["cacKyNang"].AsBsonArray.Select(x => x.ToString()).ToArray();
                        var ungVien = new UngVien
                        {
                            soCCCD = ungVienBson["soCCCD"].AsString,
                            hoTen = ungVienBson["hoTen"].AsString,
                            ngaySinh = DateTime.Parse(ungVienBson["ngaySinh"].AsString),
                            diaChi = ungVienBson["diaChi"].AsString,
                            email = ungVienBson["email"].AsString,
                            soDienThoai = ungVienBson["soDienThoai"].AsString,
                            hocVan = ungVienBson["hocVan"].AsString,
                            chuyenNganh = ungVienBson["chuyenNganh"].AsString,
                            GPA = ungVienBson["GPA"].AsString,
                            cacKyNang = cacKyNangArray.ToList(),
                            chiTietKyNang = ungVienBson["chiTietKyNang"].AsString,
                            kinhNghiemLamViec = ungVienBson["kinhNghiemLamViec"].AsString,
                            cacDuAn = ungVienBson["cacDuAn"].AsString,
                            mucTieuCaNhan = ungVienBson["mucTieuCaNhan"].AsString
                        };

                        danhSachUngVien.Add(ungVien);
                    }
                }
            }
            return danhSachUngVien;
        }


        public int xoaUngVien(string soCCCD)
        {
            var filter = Builders<BsonDocument>.Filter.ElemMatch("danhSachUngVien", Builders<BsonDocument>.Filter.Eq("soCCCD", soCCCD));

            var update = Builders<BsonDocument>.Update.PullFilter("danhSachUngVien", Builders<BsonDocument>.Filter.Eq("soCCCD", soCCCD));

            var updateResult = collection.UpdateMany(filter, update);

            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return 1; // Xóa thành công
            }
            else
            {
                return 0; // Không xóa hoặc có lỗi xảy ra
            }
        }

    }
}
