using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
        public void Serialize<T>(T source, string xmlFilePath, XmlSerializerNamespaces? namespaces, Encoding? encoding)
        {
            try
            {
                using (var fileStream = this.fileSystem.OpenOrCreateFile(xmlFilePath, FileAccess.Write, FileShare.None))
                {
                    using(var streamWriter = new StreamWriter(fileStream, encoding ?? Encoding.UTF8))
                    {
                        var serializer = new XmlSerializer(typeof(T));
                        serializer.Serialize(streamWriter, source, namespaces);
                    }
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
        public void Serialize<T>(T source, Stream targetStream, XmlSerializerNamespaces? namespaces, Encoding? encoding)
        {
            var xmlWriterSettings = new XmlWriterSettings()
            {
                Encoding = encoding ?? Encoding.UTF8,
                Indent = true,
                OmitXmlDeclaration = false,
            };

            try
            {
                using (var xmlWriter = XmlWriter.Create(targetStream, xmlWriterSettings))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(xmlWriter, source, namespaces);
                }
            }
            catch (InvalidOperationException)
            {
                // Serialization error
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        /// <inheritdoc />
        public bool TrySerialize<T>(T source, string xmlFilePath, XmlSerializerNamespaces? namespaces, Encoding? encoding)
        {
            try
            {
                this.Serialize(source, xmlFilePath, namespaces, encoding);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool TrySerialize<T>(T source, Stream targetStream, XmlSerializerNamespaces? namespaces, Encoding? encoding)
        {
            try
            {
                this.Serialize(source, targetStream, namespaces, encoding);
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
            var serializer = new XmlSerializer(typeof(T));
            try
            {
                using (var fileStream = this.fileSystem.OpenReadFile(xmlFilePath))
                {
                    using(var xmlReader = XmlReader.Create(fileStream))
                    {
                        return (T)serializer.Deserialize(xmlReader);
                    }
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
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(xmlStream);
        }

        /// <inheritdoc />
        public T Deserialize<T>(TextReader textReader)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(textReader);
        }

        /// <inheritdoc />
        public bool TryDeserialize<T>(string xmlFilePath, out T deserializedObject)
        {
            deserializedObject = default;
            try
            {
                deserializedObject = this.Deserialize<T>(xmlFilePath);
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
                deserializedObject = this.Deserialize<T>(xmlStream);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool TryDeserialize<T>(TextReader textReader, out T deserializedObject)
        {
            deserializedObject = default;
            try
            {
                deserializedObject = this.Deserialize<T>(textReader);
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
