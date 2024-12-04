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
    
    
    [System.Text.Json.Serialization.JsonConverter(typeof(MeshPrimitiveConverter))]
    public class MeshPrimitive {
        
        /// <summary>
        /// Backing field for Attributes.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, int> m_attributes;
        
        /// <summary>
        /// Backing field for Indices.
        /// </summary>
        private System.Nullable<int> m_indices;
        
        /// <summary>
        /// Backing field for Material.
        /// </summary>
        private System.Nullable<int> m_material;
        
        /// <summary>
        /// Backing field for Mode.
        /// </summary>
        private ModeEnum m_mode = ModeEnum.TRIANGLES;
        
        /// <summary>
        /// Backing field for Targets.
        /// </summary>
        private System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, int>> m_targets;
        
        /// <summary>
        /// Backing field for Extensions.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, object> m_extensions;
        
        /// <summary>
        /// Backing field for Extras.
        /// </summary>
        private Extras m_extras;
        
        /// <summary>
        /// A plain JSON object, where each key corresponds to a mesh attribute semantic and each value is the index of the accessor containing attribute's data.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("attributes")]
        public System.Collections.Generic.Dictionary<string, int> Attributes {
            get {
                return this.m_attributes;
            }
            set {
                this.m_attributes = value;
            }
        }
        
        /// <summary>
        /// The index of the accessor that contains the vertex indices.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("indices")]
        public System.Nullable<int> Indices {
            get {
                return this.m_indices;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("Indices", value, "Expected value to be greater than or equal to 0");
                }
                this.m_indices = value;
            }
        }
        
        /// <summary>
        /// The index of the material to apply to this primitive when rendering.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("material")]
        public System.Nullable<int> Material {
            get {
                return this.m_material;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("Material", value, "Expected value to be greater than or equal to 0");
                }
                this.m_material = value;
            }
        }
        
        /// <summary>
        /// The topology type of primitives to render.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("mode")]
        public ModeEnum Mode {
            get {
                return this.m_mode;
            }
            set {
                this.m_mode = value;
            }
        }
        
        /// <summary>
        /// An array of morph targets.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("targets")]
        public System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, int>> Targets {
            get {
                return this.m_targets;
            }
            set {
                if ((value == null)) {
                    this.m_targets = value;
                    return;
                }
                if ((value.Count < 1u)) {
                    throw new System.ArgumentException("List not long enough");
                }
                this.m_targets = value;
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
        
        public bool ShouldSerializeAttributes() {
            return ((m_attributes == null) 
                        == false);
        }
        
        public bool ShouldSerializeIndices() {
            return ((m_indices == null) 
                        == false);
        }
        
        public bool ShouldSerializeMaterial() {
            return ((m_material == null) 
                        == false);
        }
        
        public bool ShouldSerializeMode() {
            return ((m_mode == ModeEnum.TRIANGLES) 
                        == false);
        }
        
        public bool ShouldSerializeTargets() {
            return ((m_targets == null) 
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
        
        public enum ModeEnum {
            
            POINTS = 0,
            
            LINES = 1,
            
            LINE_LOOP = 2,
            
            LINE_STRIP = 3,
            
            TRIANGLES = 4,
            
            TRIANGLE_STRIP = 5,
            
            TRIANGLE_FAN = 6,
        }
        
        public class MeshPrimitiveConverter : JsonConverter<MeshPrimitive> {
            
            public override bool CanConvert(System.Type typeToConvert) {
                return (typeof(MeshPrimitive) == typeToConvert);
            }
            
            public override MeshPrimitive Read(ref Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options) {
                if ((reader.TokenType != JsonTokenType.StartObject)) {
                    throw new JsonException();
                }
                MeshPrimitive foo = new MeshPrimitive();
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
                        if ((propertyName == "attributes")) {
                            foo.Attributes = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, int>>(ref reader, options);
                        }
                        if ((propertyName == "indices")) {
                            foo.Indices = System.Text.Json.JsonSerializer.Deserialize<System.Nullable<int>>(ref reader, options);
                        }
                        if ((propertyName == "material")) {
                            foo.Material = System.Text.Json.JsonSerializer.Deserialize<System.Nullable<int>>(ref reader, options);
                        }
                        if ((propertyName == "mode")) {
                            foo.Mode = System.Text.Json.JsonSerializer.Deserialize<ModeEnum>(ref reader, options);
                        }
                        if ((propertyName == "targets")) {
                            foo.Targets = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, int>>>(ref reader, options);
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
            
            public override void Write(System.Text.Json.Utf8JsonWriter writer, MeshPrimitive value, System.Text.Json.JsonSerializerOptions options) {
                writer.WriteStartObject();
                writer.WritePropertyName("attributes");
                System.Text.Json.JsonSerializer.Serialize(writer, value.Attributes, options);
                if (value.ShouldSerializeIndices()) {
                    writer.WritePropertyName("indices");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Indices, options);
                }
                if (value.ShouldSerializeMaterial()) {
                    writer.WritePropertyName("material");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Material, options);
                }
                if (value.ShouldSerializeMode()) {
                    writer.WritePropertyName("mode");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Mode, options);
                }
                if (value.ShouldSerializeTargets()) {
                    writer.WritePropertyName("targets");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Targets, options);
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
