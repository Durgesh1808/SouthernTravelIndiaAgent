using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SouthernTravelIndiaAgent
{
    public interface Instamojo
    {
        /**
          * Create a Payment Order request Create a Payment Order request response.
          *
          * @param paymentRequest the payment request
          * @return the create payment request response
          */
        CreatePaymentOrderResponse createNewPaymentRequest(PaymentOrder objPaymentRequest);

        /**
          * Get all your payment orders.
          *
          * @return the payment request status
          */
        PaymentOrderListResponse getPaymentOrderList(PaymentOrderListRequest objPaymentOrderListRequest);

        /**
          * Get details of this payment order.
          *
          * @param strPaymentId        the payment id
          * @return the payment status
          */
        PaymentOrderDetailsResponse getPaymentOrderDetails(string strOrderId);

        /**
          * Get details of this payment order of TransactionId
          *
          * @return the payment request list
          */
        PaymentOrderDetailsResponse getPaymentOrderDetailsByTransactionId(string strTransactionId);

        /**
         * Create a Refund request.
         *
         * @return the Sucess
         */
        CreateRefundResponce createNewRefundRequest(Refund objRefund);
    }


    public class AccessTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public string scope { get; set; }
        public string error { get; set; }
    }

    public class BaseException : Exception
    {
        public BaseException()
            : base() { }

        public BaseException(string message)
            : base(message) { }

        public BaseException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public BaseException(string message, Exception innerException)
            : base(message, innerException) { }

        public BaseException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    public class ConnectionException : BaseException
    {
        public ConnectionException(string message)
            : base(message)
        { }
    }

    public class CreatePaymentOrderResponse
    {
        public Order order { get; set; }
        public PaymentOptions payment_options { get; set; }
    }

    public class CreateRefundResponce
    {
        public Refund refund { get; set; }
        public bool success { get; set; }
    }

    public class InstamojoConstants
    {
        public static string INSTAMOJO_AUTH_ENDPOINT = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["INSTAMOJO_AUTH_ENDPOINT"]);//"https://www.instamojo.com/oauth2/token/";
        public static string GRANT_TYPE = "client_credentials";
        public static string INSTAMOJO_API_ENDPOINT = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["INSTAMOJO_API_ENDPOINT"]);//"https://api.instamojo.com/v2/";
    }

    public class InvalidPaymentOrderException : BaseException
    {
        private InvalidOrderResponse errorResponse;

        public InvalidPaymentOrderException()
        { }

        public InvalidPaymentOrderException(string message)
            : base(message)
        {
            try
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                errorResponse = ser.Deserialize<InvalidOrderResponse>(message);
            }
            catch (Exception)
            {
                //nothing comes here since it failed everywhere
            }
        }

        public bool IsWebhookValid()
        {
            if (errorResponse == null)
            {
                return true;
            }

            if (errorResponse.webhook_url != null)
            {
                return false;
            }

            return true;
        }

        public bool IsTransactionIDValid()
        {
            if (errorResponse == null)
            {
                return true;
            }

            if (errorResponse.transaction_id != null)
            {
                return false;
            }

            return true;
        }

        public bool IsCurrencyValid()
        {
            if (errorResponse == null)
            {
                return true;
            }

            if (errorResponse.currency != null)
            {
                return false;
            }

            return true;
        }

    }

    class InvalidOrderResponse
    {
        public string[] transaction_id { get; set; }
        public string[] webhook_url { get; set; }
        public string[] currency { get; set; }
    }

    public class JavaScriptConverters
    {
        public class NullPropertiesConverter : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                var jsonExample = new Dictionary<string, object>();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    //check if decorated with ScriptIgnore attribute
                    bool ignoreProp = prop.IsDefined(typeof(ScriptIgnoreAttribute), true);

                    var value = prop.GetValue(obj, BindingFlags.Public, null, null, null);
                    if (value != null && !ignoreProp)
                        jsonExample.Add(prop.Name, value);
                }

                return jsonExample;
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get { return GetType().Assembly.GetTypes(); }
            }

        }
    }


    public class Order
    {

        public string id { get; set; }

        public string transaction_id { get; set; }
        public string status { get; set; }
        public string currency { get; set; }
        public double? amount { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string description { get; set; }
        public string redirect_url { get; set; }
        public string webhook_url { get; set; }
        public string created_at { get; set; }
        public string resource_uri { get; set; }

    }

    public class PaymentOptions
    {
        public string payment_url { get; set; }
    }

    public class PaymentOrder
    {
        public PaymentOrder()
        {
            currency = "INR";
        }

        /** The Constant serialVersionUID. */
        private static long serialVersionUID = 4912793214890694717L;

        private static Regex TRANSACTION_ID_MATCHER = new Regex("[A-Za-z0-9_-]+");
        private static Regex EMAIL_MATCHER = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$");
        private static Regex PHONE_NUMBER_MATCHER = new Regex("^[0-9]{10}$");
        private static Regex URL_MATCHER = new Regex("^(https?|ftp|file)://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]");
        private static double MIN_AMOUNT = 9.00;
        private static int MAX_TID_CHAR_LIMIT = 64;
        private static int MAX_EMAIL_CHAR_LIMIT = 75;
        private static int MAX_NAME_CHAR_LIMIT = 100;
        private static int MAX_DESCRIPTION_CHAR_LIMIT = 255;
        private static string[] CurrencyArray = { "INR", "USD" };


        //Required POST parameters
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public double? amount { get; set; }
        public string transaction_id { get; set; }
        public string redirect_url { get; set; }
        public string webhook_url { get; set; }
        public string currency { get; set; }

        //Extra POST parameters 

        public string description { get; set; }


        public bool nameInvalid;
        public bool emailInvalid;
        public bool phoneInvalid;
        public bool transactionIdInvalid;
        public bool descriptionInvalid;
        public bool currencyInvalid;
        public bool amountInvalid;
        public bool redirectUrlInvalid;
        public bool webhookUrlInvalid;


        /**
     * Validate.
     */
        public bool validate()
        {
            bool invalid = false;

            if (string.IsNullOrEmpty(transaction_id) || !TRANSACTION_ID_MATCHER.IsMatch(transaction_id) || transaction_id.Length > MAX_TID_CHAR_LIMIT)
            {
                invalid = true;
                transactionIdInvalid = true;
            }
            if (string.IsNullOrEmpty(email) || !EMAIL_MATCHER.IsMatch(email) || email.Length > MAX_EMAIL_CHAR_LIMIT)
            {
                invalid = true;
                emailInvalid = true;
            }

            if (string.IsNullOrEmpty(name) || name.Length > MAX_NAME_CHAR_LIMIT)
            {
                invalid = true;
                nameInvalid = true;
            }
            if (string.IsNullOrEmpty(currency) || !CurrencyArray.Any(currency.Contains))
            {
                invalid = true;
                currencyInvalid = true;
            }
            if (string.IsNullOrEmpty(phone) || !PHONE_NUMBER_MATCHER.IsMatch(phone))
            {
                invalid = true;
                phoneInvalid = true;
            }
            if (amount == null || amount < MIN_AMOUNT)
            {
                invalid = true;
                amountInvalid = true;
            }
            if (!string.IsNullOrEmpty(description) && description.Length > MAX_DESCRIPTION_CHAR_LIMIT)
            {
                invalid = true;
                descriptionInvalid = true;
            }
            if (string.IsNullOrEmpty(redirect_url) || !URL_MATCHER.IsMatch(redirect_url))
            {
                invalid = true;
                redirectUrlInvalid = true;
            }
            if (!string.IsNullOrEmpty(webhook_url) && !URL_MATCHER.IsMatch(webhook_url))
            {
                invalid = true;
                webhookUrlInvalid = true;
            }

            return invalid;
        }

    }


    public class PaymentOrderDetailsResponse
    {
        public PaymentOrderDetailsResponse()
        {
            status = "pending";
            currency = "INR";
        }

        public string id { get; set; }
        public string transaction_id { get; set; }
        public string status { get; set; }
        public string currency { get; set; }
        public decimal? amount { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string description { get; set; }
        public string redirect_url { get; set; }

        public object payments { get; set; }

        public string created_at { get; set; }
        public string resource_uri { get; set; }
    }

    public class PaymentOrderListRequest
    {
        public PaymentOrderListRequest()
        {
            // Default Values
            page = 1;
            limit = 50;
        }

        public string id { get; set; }
        public string transaction_id { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

    public class PaymentOrderListResponse
    {

        public int? count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public Order[] orders { get; set; }

    }

    public class Refund
    {
        public Refund()
        {
            // Default Values
            total_amount = 9;
        }

        private static string[] typeArray = { "RFD", "TNR", "QFL", "QNR", "EWN", "TAN", "PTH" };
        private static double MIN_AMOUNT = 9.00;

        public string id { get; set; }
        public string payment_id { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string body { get; set; }
        public double? refund_amount { get; set; }
        public double? total_amount { get; set; }
        public string created_at { get; set; }


        public bool idInvalid;
        public bool payment_idInvalid;
        public bool statusInvalid;
        public bool typeInvalid;
        public bool bodyInvalid;
        public bool refund_amountInvalid;
        public bool total_amountInvalid;
        public bool created_atInvalid;

        /**
        * Validate.
        */
        public bool validate()
        {
            bool invalid = false;

            if (string.IsNullOrEmpty(payment_id))
            {
                invalid = true;
                payment_idInvalid = true;
            }
            if (string.IsNullOrEmpty(type) || !typeArray.Any(type.Contains))
            {
                invalid = true;
                typeInvalid = true;
            }
            if (string.IsNullOrEmpty(body))
            {
                invalid = true;
                bodyInvalid = true;
            }
            if (refund_amount == null || refund_amount < MIN_AMOUNT)
            {
                invalid = true;
                refund_amountInvalid = true;
            }
            return invalid;
        }
    }

    public class PaymentsDetails
    {
        public string id { get; set; }
        public string status { get; set; }
        public string instrument_type { get; set; }
        public string billing_instrument { get; set; }
        public object failure { get; set; }
    }
}
