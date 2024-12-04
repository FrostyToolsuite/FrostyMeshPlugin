//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FrostyMeshPlugin.Gltf.AutoGenerated {
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    
    
    [System.Text.Json.Serialization.JsonConverter(typeof(BufferViewConverter))]
    public class BufferView {
        
        /// <summary>
        /// Backing field for Buffer.
        /// </summary>
        private int m_buffer;
        
        /// <summary>
        /// Backing field for ByteOffset.
        /// </summary>
        private int m_byteOffset = 0;
        
        /// <summary>
        /// Backing field for ByteLength.
        /// </summary>
        private int m_byteLength;
        
        /// <summary>
        /// Backing field for ByteStride.
        /// </summary>
        private System.Nullable<int> m_byteStride;
        
        /// <summary>
        /// Backing field for Target.
        /// </summary>
        private System.Nullable<TargetEnum> m_target;
        
        /// <summary>
        /// Backing field for Name.
        /// </summary>
        private string m_name;
        
        /// <summary>
        /// Backing field for Extensions.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, object> m_extensions;
        
        /// <summary>
        /// Backing field for Extras.
        /// </summary>
        private Extras m_extras;
        
        /// <summary>
        /// The index of the buffer.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("buffer")]
        public int Buffer {
            get {
                return this.m_buffer;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("Buffer", value, "Expected value to be greater than or equal to 0");
                }
                this.m_buffer = value;
            }
        }
        
        /// <summary>
        /// The offset into the buffer in bytes.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("byteOffset")]
        public int ByteOffset {
            get {
                return this.m_byteOffset;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("ByteOffset", value, "Expected value to be greater than or equal to 0");
                }
                this.m_byteOffset = value;
            }
        }
        
        /// <summary>
        /// The length of the bufferView in bytes.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("byteLength")]
        public int ByteLength {
            get {
                return this.m_byteLength;
            }
            set {
                if ((value < 1)) {
                    throw new System.ArgumentOutOfRangeException("ByteLength", value, "Expected value to be greater than or equal to 1");
                }
                this.m_byteLength = value;
            }
        }
        
        /// <summary>
        /// The stride, in bytes.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("byteStride")]
        public System.Nullable<int> ByteStride {
            get {
                return this.m_byteStride;
            }
            set {
                if ((value < 4)) {
                    throw new System.ArgumentOutOfRangeException("ByteStride", value, "Expected value to be greater than or equal to 4");
                }
                if ((value > 252)) {
                    throw new System.ArgumentOutOfRangeException("ByteStride", value, "Expected value to be less than or equal to 252");
                }
                this.m_byteStride = value;
            }
        }
        
        /// <summary>
        /// The hint representing the intended GPU buffer type to use with this buffer view.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("target")]
        public System.Nullable<TargetEnum> Target {
            get {
                return this.m_target;
            }
            set {
                this.m_target = value;
            }
        }
        
        /// <summary>
        /// The user-defined name of this object.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("name")]
        public string Name {
            get {
                return this.m_name;
            }
            set {
                this.m_name = value;
            }
        }
        
        /// <summary>
        /// JSON object with extension-specific objects.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("extensions")]
        public System.Collections.Generic.Dictionary<string, object> Extensions {
            get {
                return this.m_extensions;
            }
            set {
                this.m_extensions = value;
            }
        }
        
        /// <summary>
        /// Application-specific data.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("extras")]
        public Extras Extras {
            get {
                return this.m_extras;
            }
            set {
                this.m_extras = value;
            }
        }
        
        public bool ShouldSerializeByteOffset() {
            return ((m_byteOffset == 0) 
                        == false);
        }
        
        public bool ShouldSerializeByteStride() {
            return ((m_byteStride == null) 
                        == false);
        }
        
        public bool ShouldSerializeTarget() {
            return ((m_target == null) 
                        == false);
        }
        
        public bool ShouldSerializeName() {
            return ((m_name == null) 
                        == false);
        }
        
        public bool ShouldSerializeExtensions() {
            return ((m_extensions == null) 
                        == false);
        }
        
        public bool ShouldSerializeExtras() {
            return ((m_extras == null) 
                        == false);
        }
        
        public enum TargetEnum {
            
            ARRAY_BUFFER = 34962,
            
            ELEMENT_ARRAY_BUFFER = 34963,
        }
        
        public class BufferViewConverter : JsonConverter<BufferView> {
            
            public override bool CanConvert(System.Type typeToConvert) {
                return (typeof(BufferView) == typeToConvert);
            }
            
            public override BufferView Read(ref Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options) {
                if ((reader.TokenType != JsonTokenType.StartObject)) {
                    throw new JsonException();
                }
                BufferView foo = new BufferView();
                for (
                ; true; 
                ) {
                    if ((reader.Read() == false)) {
break;
                    }
                    if ((reader.TokenType == JsonTokenType.EndObject)) {
break;
                    }
                    if ((reader.TokenType == JsonTokenType.PropertyName)) {
                        string propertyName = reader.GetString();
                        reader.Read();
                        if ((propertyName == "buffer")) {
                            foo.Buffer = System.Text.Json.JsonSerializer.Deserialize<int>(ref reader, options);
                        }
                        if ((propertyName == "byteOffset")) {
                            foo.ByteOffset = System.Text.Json.JsonSerializer.Deserialize<int>(ref reader, options);
                        }
                        if ((propertyName == "byteLength")) {
                            foo.ByteLength = System.Text.Json.JsonSerializer.Deserialize<int>(ref reader, options);
                        }
                        if ((propertyName == "byteStride")) {
                            foo.ByteStride = System.Text.Json.JsonSerializer.Deserialize<System.Nullable<int>>(ref reader, options);
                        }
                        if ((propertyName == "target")) {
                            foo.Target = System.Text.Json.JsonSerializer.Deserialize<System.Nullable<TargetEnum>>(ref reader, options);
                        }
                        if ((propertyName == "name")) {
                            foo.Name = System.Text.Json.JsonSerializer.Deserialize<string>(ref reader, options);
                        }
                        if ((propertyName == "extensions")) {
                            foo.Extensions = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, object>>(ref reader, options);
                        }
                        if ((propertyName == "extras")) {
                            foo.Extras = System.Text.Json.JsonSerializer.Deserialize<Extras>(ref reader, options);
                        }
                    }
                }
                return foo;
            }
            
            public override void Write(System.Text.Json.Utf8JsonWriter writer, BufferView value, System.Text.Json.JsonSerializerOptions options) {
                writer.WriteStartObject();
                writer.WritePropertyName("buffer");
                System.Text.Json.JsonSerializer.Serialize(writer, value.Buffer, options);
                if (value.ShouldSerializeByteOffset()) {
                    writer.WritePropertyName("byteOffset");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.ByteOffset, options);
                }
                writer.WritePropertyName("byteLength");
                System.Text.Json.JsonSerializer.Serialize(writer, value.ByteLength, options);
                if (value.ShouldSerializeByteStride()) {
                    writer.WritePropertyName("byteStride");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.ByteStride, options);
                }
                if (value.ShouldSerializeTarget()) {
                    writer.WritePropertyName("target");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Target, options);
                }
                if (value.ShouldSerializeName()) {
                    writer.WritePropertyName("name");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Name, options);
                }
                if (value.ShouldSerializeExtensions()) {
                    writer.WritePropertyName("extensions");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Extensions, options);
                }
                if (value.ShouldSerializeExtras()) {
                    writer.WritePropertyName("extras");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Extras, options);
                }
                writer.WriteEndObject();
            }
        }
    }
}
