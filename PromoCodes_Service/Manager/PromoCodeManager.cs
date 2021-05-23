using Microsoft.Extensions.Configuration;
using PromoCodes_Service.Helper;
using PromoCodes_Service.Interfaces;
using PromoCodes_Service.Models;
using PromoCodes_Service.Models.ViewModel.Enum;
using PromoCodes_Service.Models.ViewModel.RequestModels;
using PromoCodes_Service.Models.ViewModel.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodes_Service.Manager
{
    public class PromoCodeManager
    {
        private readonly IConfiguration _configuration;
        EVoucherSystemDBContext _dbContext;
        IRepository<PurchaseOrderTb> _purchaseOrdrerepo;
        IRepository<GeneratedEvoucherTb> _generatedEvrepo;

        public PromoCodeManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new EVoucherSystemDBContext();
            _purchaseOrdrerepo = new BaseRepository<PurchaseOrderTb>(_dbContext);
            _generatedEvrepo = new BaseRepository<GeneratedEvoucherTb>(_dbContext);
        }

        public GeneratePromoCodeResponse GeneratePromoCode(GeneratePromoCodeRequest _request)
        {
            GeneratePromoCodeResponse response = new GeneratePromoCodeResponse();
            var porder = _purchaseOrdrerepo.Get.Where(p => p.PurchaseOrderNo == _request.PurchaseOrder_No && p.VoucherGenerated == false).FirstOrDefault();

            if (porder != null)
            {
                for (int i = 0; i < porder.Quantity; i++)
                {
                    bool isUnique = false;
                    string promoCode = StringHelper.GeneatePromo();
                    string qrCodePath;
                    do
                    {
                        isUnique = !(from gp in _generatedEvrepo.Get
                                     where gp.PromoCode == promoCode
                                     select true
                                   ).FirstOrDefault();
                    } while (!isUnique);

                    qrCodePath = QRCodeHelper.GenerateQRCode(promoCode);

                    if (qrCodePath != "")
                    {
                        GeneratedEvoucherTb generatedEvoucher = new GeneratedEvoucherTb
                        {
                            PromoCode = promoCode,
                            PurchaseOrderNo = porder.PurchaseOrderNo,
                            VoucherNo = porder.VoucherNo,
                            QrimagePath = qrCodePath,
                            VoucherImagePath = porder.ImagePath,
                            OwnerName = porder.BuyerName,
                            OwnerPhone = porder.BuyerPhone,
                            VoncherAmount = porder.VoncherAmount,
                            ExpiryDate = porder.ExpiryDate,
                            Status = (int)RecordStatus.Active
                        };

                        _generatedEvrepo.Insert(generatedEvoucher);

                    }
                }
                porder.VoucherGenerated = true;

            }
            else
            {
                response.StatusCode = 404;
                response.ErrorType = "Not-Found";
                response.ErrorMessage = "Record Not Available.";
            }
            response.PromoCodeGenerated = true;

            return response;
        }

    }
}
