using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace ITS.Symphony.UI.QT
{
    /// <summary>
    /// 클라이언트 PC에 시스템 데이터의 저장, 로드 등을 수행하는 클래스
    /// </summary>
    class CustomizationHelperQT
    {
        /// <summary>
        /// 클라이언트 PC에 대상 개체를 데이터로 저장한다.
        /// </summary>
        /// <typeparam name="T">제네릭 개체 형식</typeparam>
        /// <param name="target">제네릭 개체 형식의 인스턴스</param>
        /// <param name="targetType">대상 개체의 Type</param>
        public static void Save<T>(T target, Type targetType)
        {
            string saveDir = System.IO.Directory.GetCurrentDirectory();
            if (Directory.Exists(saveDir) == false) {
                Directory.CreateDirectory(saveDir);
            }

            string filePath = Path.Combine(saveDir, targetType.Name + ".xml");
            using (FileStream fs = File.Open(filePath, FileMode.Create, FileAccess.Write)) {
                new XmlSerializer(targetType).Serialize(fs, target);
            }
        }

        /// <summary>
        /// 클라이언트 PC에 저장된 데이터를 로드한다.
        /// </summary>
        /// <typeparam name="T">제네릭 개체 형식</typeparam>
        /// <param name="targetType">대상 개체의 Type</param>
        /// <returns>제네릭 개체 형식의 인스턴스</returns>
        public static T Load<T>(Type targetType)
        {
            string saveDir = System.IO.Directory.GetCurrentDirectory();

            string filePath = Path.Combine(saveDir, targetType.Name + ".xml");
            if (File.Exists(filePath)) {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read)) {
                    return (T)new XmlSerializer(targetType).Deserialize(fs);
                }
            }

            return default(T);
        }
    }
}
