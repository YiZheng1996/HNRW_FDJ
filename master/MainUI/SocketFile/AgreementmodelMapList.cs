using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetorSignalSimulator.UI.SocketFile
{
    [Table(Name = "config_root")]
    public class ModelMap
    {
        public string id { get; set; }
        [JsonProperty("modelName")]
        public string ModelName { get; set; }

        [JsonProperty("equipModelId")]
        public int EquipModelId { get; set; }

        [JsonProperty("modelDetailList")]

        [Navigate(nameof(ModelDetail.ParentId))]
        public List<ModelDetail> ModelDetails { get; set; } = new List<ModelDetail>();
    }

    [Table(Name = "config_root_modelDetailList ")]
    public class ModelDetail
    {

        [Column(IsPrimary = true)]
        public int pid { get; set; }

        public int id { get; set; }

        [Column(Name = "parent_id")]
        public string ParentId { get; set; }
        /// <summary>
        /// 协议名称（数据名称）
        /// </summary>
        public string signalName { get; set; }

        /// <summary>
        /// 字偏移
        /// </summary>
        public int byteOffset { get; set; }


        /// <summary>
        /// 一般为空，若为0.1，1代表0.1，采集数*0.1
        /// </summary>
        public string dataFormat { get; set; }

        /// <summary>
        /// 位偏移
        /// </summary>
        public int binaryOffset { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string dataType { get; set; }

        /// <summary>
        /// 车辆名称T1
        /// </summary>
        public string vehicleNo { get; set; }

        /// <summary>
        /// 车辆号 1
        /// </summary>
        public int carNo { get; set; }

        /// <summary>
        /// 变量顺序号
        /// </summary>
        public string sortNo { get; set; }

    }
    public class ModelStorage
    {
        private readonly Dictionary<string, ModelMap> _models = new Dictionary<string, ModelMap>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 添加/更新模型（强制名称唯一性）
        /// </summary>
        public void AddModel(ModelMap model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(model.ModelName))
                throw new ArgumentException("模型名称不能为空");

            _models[model.ModelName.Trim()] = model;
        }

        /// <summary>
        /// 根据模型名称尝试获取模型
        /// </summary>
        public bool TryGetModelByName(string modelName, out ModelMap model)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                model = null;
                return false;
            }

            return _models.TryGetValue(modelName.Trim(), out model);
        }

        /// <summary>
        /// 获取所有模型列表
        /// </summary>
        public IEnumerable<ModelMap> GetAllModels() => _models.Values;
    }
}
