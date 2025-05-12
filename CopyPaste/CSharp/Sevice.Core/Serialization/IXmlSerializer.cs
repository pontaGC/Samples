using System.Xml.Serialization;

namespace Services.Core.Serialization
{
    /// <summary>
    /// Responsible for serializing or deserializing the object related to XML Document.sw
    /// </summary>
    public interface IXmlSerializer
    {
        /// <summary>
        /// Serializes the source object to a target stream.
        /// </summary>
        /// <typeparam name="T">The type of a source object.</typeparam>
        /// <param name="source">The object to serialize. If <c>source</c> is <c>null</c>, this method does nothing.</param>
        /// <param name="xmlFilePath">The XML document to serialize.</param>
        /// <param name="namespaces">The namespaces for then generated XML document.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="xmlFilePath"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The file extension <paramref name="xmlFilePath"/> is not XML extension.</exception>
        /// <exception cref="InvalidOperationException">An error occurred during serialization. The original exception is available using the <c>InnerException</c> property.</exception>
        void Serialize<T>(T source, string xmlFilePath, XmlSerializerNamespaces? namespaces = null);

        /// <summary>
        /// Serializes the source object to a target stream.
        /// </summary>
        /// <typeparam name="T">The type of a source object.</typeparam>
        /// <param name="source">The object to serialize. If <c>source</c> is <c>null</c>, this method does nothing.</param>
        /// <param name="targetStream">The target stream to serialize.</param>
        /// <param name="namespaces">The namespaces for then generated XML document.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="targetStream"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">An error occurred during serialization. The original exception is available using the <c>InnerException</c> property.</exception>
        void Serialize<T>(T source, Stream targetStream, XmlSerializerNamespaces? namespaces = null);

        /// <summary>
        /// Try serializing a target object to a target XML file.
        /// </summary>
        /// <typeparam name="T">The type of a source object.</typeparam>
        /// <param name="source">The object to serialize. If <c>source</c> is <c>null</c>, this method does nothing.</param>
        /// <param name="xmlFilePath">The XML document to serialize.</param>
        /// <param name="namespaces">The namespaces for then generated XML document.</param>
        /// <returns><c>true</c>, if the serialization is success, Otherwise; <c>false</c>.</returns>
        bool TrySerialize<T>(T source, string xmlFilePath, XmlSerializerNamespaces? namespaces = null);

        /// <summary>
        /// Try serializing the source object to a target stream.
        /// </summary>
        /// <typeparam name="T">The type of a source object.</typeparam>
        /// <param name="source">The object to serialize. If <c>source</c> is <c>null</c>, this method does nothing.</param>
        /// <param name="targetStream">The target stream to serialize.</param>
        /// <param name="namespaces">The namespaces for then generated XML document.</param>
        /// <returns><c>true</c>, if the serialization is success, Otherwise; <c>false</c>.</returns>
        bool TrySerialize<T>(T source, Stream targetStream, XmlSerializerNamespaces? namespaces = null);

        /// <summary>
        /// Deserializes the specified XML document to create the target object.
        /// </summary>
        /// <typeparam name="T">The type of a target object deserialized.</typeparam>
        /// <param name="xmlFilePath">The XML document to deserialize.</param>
        /// <returns>The deserialized result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="xmlFilePath"/> is <c>null</c> or an empty string.</exception>
        /// <exception cref="ArgumentException">The file extension <paramref name="xmlFilePath"/> is not XML extension.</exception>
        /// <exception cref="InvalidOperationException">An error occurred during deserialization. The original exception is available using the <c>InnerException</c> property.</exception>
        T Deserialize<T>(string xmlFilePath);

        /// <summary>
        /// Deserializes the specified XML document to create the target object.
        /// </summary>
        /// <typeparam name="T">The type of a target object deserialized.</typeparam>
        /// <param name="xmlStream">The XML document stream to deserialize.</param>
        /// <returns>The deserialized result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="xmlStream"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">An error occurred during deserialization. The original exception is available using the <c>InnerException</c> property.</exception>
        T Deserialize<T>(Stream xmlStream);

        /// <summary>
        /// Deserializes the specified XML document to create the target object.
        /// </summary>
        /// <typeparam name="T">The type of a target object deserialized.</typeparam>
        /// <param name="xmlFilePath">The XML document to deserialize.</param>
        /// <param name="deserializedObject">The deserialized object which type is <c>T</c>.</param>
        /// <returns><c>true</c>, if the deserialization is success, Otherwise; <c>false</c>.</returns>
        bool TryDeserialize<T>(string xmlFilePath, out T deserializedObject);

        /// <summary>
        /// Deserializes the specified XML document to create the target object.
        /// </summary>
        /// <typeparam name="T">The type of a target object deserialized.</typeparam>
        /// <param name="xmlStream">The XML document stream to deserialize.</param>
        /// <param name="deserializedObject">The deserialized object which type is <c>T</c>.</param>
        /// <returns><c>true</c>, if the deserialization is success, Otherwise; <c>false</c>.</returns>
        bool TryDeserialize<T>(Stream xmlStream, out T deserializedObject);
    }
}
