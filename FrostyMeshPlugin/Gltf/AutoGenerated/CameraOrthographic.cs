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
    
    
    [System.Text.Json.Serialization.JsonConverter(typeof(CameraOrthographicConverter))]
    public class CameraOrthographic {
        
        /// <summary>
        /// Backing field for Xmag.
        /// </summary>
        private float m_xmag;
        
        /// <summary>
        /// Backing field for Ymag.
        /// </summary>
        private float m_ymag;
        
        /// <summary>
        /// Backing field for Zfar.
        /// </summary>
        private float m_zfar;
        
        /// <summary>
        /// Backing field for Znear.
        /// </summary>
        private float m_znear;
        
        /// <summary>
        /// Backing field for Extensions.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, object> m_extensions;
        
        /// <summary>
        /// Backing field for Extras.
        /// </summary>
        private Extras m_extras;
        
        /// <summary>
        /// The floating-point horizontal magnification of the view. This value **MUST NOT** be equal to zero. This value **SHOULD NOT** be negative.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("xmag")]
        public float Xmag {
            get {
                return this.m_xmag;
            }
            set {
                this.m_xmag = value;
            }
        }
        
        /// <summary>
        /// The floating-point vertical magnification of the view. This value **MUST NOT** be equal to zero. This value **SHOULD NOT** be negative.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("ymag")]
        public float Ymag {
            get {
                return this.m_ymag;
            }
            set {
                this.m_ymag = value;
            }
        }
        
        /// <summary>
        /// The floating-point distance to the far clipping plane. This value **MUST NOT** be equal to zero. `zfar` **MUST** be greater than `znear`.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("zfar")]
        public float Zfar {
            get {
                return this.m_zfar;
            }
            set {
                if ((value <= 0D)) {
                    throw new System.ArgumentOutOfRangeException("Zfar", value, "Expected value to be greater than 0");
                }
                this.m_zfar = value;
            }
        }
        
        /// <summary>
        /// The floating-point distance to the near clipping plane.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("znear")]
        public float Znear {
            get {
                return this.m_znear;
            }
            set {
                if ((value < 0D)) {
                    throw new System.ArgumentOutOfRangeException("Znear", value, "Expected value to be greater than or equal to 0");
                }
                this.m_znear = value;
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
        
        public bool ShouldSerializeExtensions() {
            return ((m_extensions == null) 
                        == false);
        }
        
        public bool ShouldSerializeExtras() {
            return ((m_extras == null) 
                        == false);
        }
        
        public class CameraOrthographicConverter : JsonConverter<CameraOrthographic> {
            
            public override bool CanConvert(System.Type typeToConvert) {
                return (typeof(CameraOrthographic) == typeToConvert);
            }
            
            public override CameraOrthographic Read(ref Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options) {
                if ((reader.TokenType != JsonTokenType.StartObject)) {
                    throw new JsonException();
                }
                CameraOrthographic foo = new CameraOrthographic();
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
                        if ((propertyName == "xmag")) {
                            foo.Xmag = System.Text.Json.JsonSerializer.Deserialize<float>(ref reader, options);
                        }
                        if ((propertyName == "ymag")) {
                            foo.Ymag = System.Text.Json.JsonSerializer.Deserialize<float>(ref reader, options);
                        }
                        if ((propertyName == "zfar")) {
                            foo.Zfar = System.Text.Json.JsonSerializer.Deserialize<float>(ref reader, options);
                        }
                        if ((propertyName == "znear")) {
                            foo.Znear = System.Text.Json.JsonSerializer.Deserialize<float>(ref reader, options);
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
            
            public override void Write(System.Text.Json.Utf8JsonWriter writer, CameraOrthographic value, System.Text.Json.JsonSerializerOptions options) {
                writer.WriteStartObject();
                writer.WritePropertyName("xmag");
                System.Text.Json.JsonSerializer.Serialize(writer, value.Xmag, options);
                writer.WritePropertyName("ymag");
                System.Text.Json.JsonSerializer.Serialize(writer, value.Ymag, options);
                writer.WritePropertyName("zfar");
                System.Text.Json.JsonSerializer.Serialize(writer, value.Zfar, options);
                writer.WritePropertyName("znear");
                System.Text.Json.JsonSerializer.Serialize(writer, value.Znear, options);
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
