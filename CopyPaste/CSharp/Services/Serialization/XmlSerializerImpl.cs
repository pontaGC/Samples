using System.Xml.Serialization;

using Services.Core.Helpers;
using Services.Core.Serialization;

namespace Services.Serialization
{
    /// <summary>
    /// Responsible for serializing or deserializing the object related to XML document.
    /// </summary>
    internal sealed class XmlSerializerImpl : IXmlSerializer
    {
        #region Fields

        private readonly IFileSystem fileSystem;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSerializerImpl"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public XmlSerializerImpl(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public void Serialize<T>(T source, string xmlFilePath, XmlSerializerNamespaces namespaces = null)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(xmlFilePath))
            {
                throw new ArgumentNullException(nameof(xmlFilePath));
            }

            if (!FileTypeHelper.IsXmlFile(xmlFilePath))
            {
                throw new ArgumentException("This file is not XML.", nameof(xmlFilePath));
            }

            var serializer = new XmlSerializer(typeof(T));
            try
            {
                using (var fileStream = this.fileSystem.OpenOrCreateFile(xmlFilePath, FileAccess.Write, FileShare.None))
                {
                    serializer.Serialize(fileStream, source, namespaces);
                }
            }
            catch (InvalidOperationException)
            {
                // Serialization error
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"File system error : {xmlFilePath}", ex);
            }
        }

        /// <inheritdoc />
        public void Serialize<T>(T source, Stream targetStream, XmlSerializerNamespaces namespaces = null)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (targetStream is null)
            {
                throw new ArgumentNullException(nameof(targetStream));
            }

            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(targetStream, source, namespaces);
        }

        /// <inheritdoc />
        public bool TrySerialize<T>(T source, string xmlFilePath, XmlSerializerNamespaces namespaces = null)
        {
            try
            {
                this.Serialize(source, xmlFilePath, namespaces);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool TrySerialize<T>(T source, Stream targetStream, XmlSerializerNamespaces namespaces = null)
        {
            try
            {
                this.Serialize(source, targetStream, namespaces);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc />
        public T Deserialize<T>(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath))
            {
                throw new ArgumentNullException(nameof(xmlFilePath));
            }

            if (!FileTypeHelper.IsXmlFile(xmlFilePath))
            {
                throw new ArgumentException("This file is not XML.", nameof(xmlFilePath));
            }

            var serializer = new XmlSerializer(typeof(T));
            try
            {
                using (var fileStream = this.fileSystem.OpenReadFile(xmlFilePath))
                {
                    return (T)serializer.Deserialize(fileStream);
                }
            }
            catch (InvalidOperationException)
            {
                // Deserialization error
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"File system error : {xmlFilePath}", ex);
            }
        }

        /// <inheritdoc />
        public T Deserialize<T>(Stream xmlStream)
        {
            if (xmlStream is null)
            {
                throw new ArgumentNullException(nameof(xmlStream));
            }

            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(xmlStream);
        }

        /// <inheritdoc />
        public bool TryDeserialize<T>(string xmlFilePath, out T deserializedObject)
        {
            deserializedObject = default;
            try
            {
                deserializedObject = Deserialize<T>(xmlFilePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool TryDeserialize<T>(Stream xmlStream, out T deserializedObject)
        {
            deserializedObject = default;
            try
            {
                deserializedObject = Deserialize<T>(xmlStream);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
