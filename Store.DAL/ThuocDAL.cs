
using System.Data;

namespace Store.DAL
{
    public class ThuocDAL
    {
        private DBConnection dtProcess = new DBConnection();

        // 1. Lấy toàn bộ danh sách thuốc và đổi tên cột (Alias) cho khớp với Form
        public DataTable GetAllThuoc()
        {
            string query = "SELECT Id AS MaThuoc, Name AS TenThuoc, CategoryId AS MaNhom, Unit AS DonViTinh, Price AS DonGia, 0 AS SoLuongTon FROM Medicines";
            return dtProcess.DocBang(query);
        }

        // 2. Thêm mới thuốc (Có truyền maThuoc do người dùng nhập)
        public bool InsertThuoc(string maThuoc, string tenThuoc, string maNhom, string dvt, decimal donGia, int soLuongTon, out string dbError)
        {
            dbError = "";
            string strMaNhom = string.IsNullOrEmpty(maNhom) ? "NULL" : maNhom;
            string strDvt = string.IsNullOrEmpty(dvt) ? "NULL" : $"N'{dvt}'";

            string query = string.Format(
                "INSERT INTO Medicines(Id, CategoryId, SupplierId, BarCode, Name, ActiveIngredient, Unit, Price, IsPrescription, IsActive) VALUES ('{0}', {1}, 1, '8935001800999', N'{2}', N'Hoạt chất mặc định', {3}, {4}, 0, 1)",
                maThuoc, strMaNhom, tenThuoc, strDvt, donGia
            );

            try
            {
                dtProcess.CapNhatDuLieu(query);
                return true;
            }
            catch (Exception ex)
            {
                dbError = ex.Message;
                return false;
            }
        }

        // 3. Sửa thuốc dựa vào Id
        public bool UpdateThuoc(string maThuoc, string tenThuoc, string maNhom, string dvt, decimal donGia, int soLuongTon, out string dbError)
        {
            dbError = "";
            string strMaNhom = string.IsNullOrEmpty(maNhom) ? "NULL" : maNhom;
            string strDvt = string.IsNullOrEmpty(dvt) ? "NULL" : $"N'{dvt}'";

            string query = string.Format(
                "UPDATE Medicines SET Name = N'{1}', CategoryId = {2}, Unit = {3}, Price = {4} WHERE Id = {0}",
                maThuoc, tenThuoc, strMaNhom, strDvt, donGia
            );

            try
            {
                dtProcess.CapNhatDuLieu(query);
                return true;
            }
            catch (Exception ex)
            {
                dbError = ex.Message;
                return false;
            }
        }

        // 4. Xóa thuốc dựa vào Id
        public bool DeleteThuoc(string maThuoc, out string dbError)
        {
            dbError = "";
            string query = string.Format("DELETE FROM Medicines WHERE Id = {0}", maThuoc);

            try
            {
                dtProcess.CapNhatDuLieu(query);
                return true;
            }
            catch (Exception ex)
            {
                dbError = ex.Message;
                return false;
            }
        }

        // 5. Tìm kiếm thuốc theo Mã thuốc hoặc Tên thuốc
        public DataTable TimKiemThuoc(string tuKhoa)
        {
            string query = string.Format(
                "SELECT Id AS MaThuoc, Name AS TenThuoc, CategoryId AS MaNhom, Unit AS DonViTinh, Price AS DonGia, 0 AS SoLuongTon FROM Medicines WHERE CAST(Id AS NVARCHAR) LIKE '%{0}%' OR Name LIKE N'%{0}%'",
                tuKhoa
            );
            return dtProcess.DocBang(query);
        }
    }
}