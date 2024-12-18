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
    
    
    [System.Text.Json.Serialization.JsonConverter(typeof(NodeConverter))]
    public class Node {
        
        /// <summary>
        /// Backing field for Camera.
        /// </summary>
        private System.Nullable<int> m_camera;
        
        /// <summary>
        /// Backing field for Children.
        /// </summary>
        private System.Collections.Generic.List<int> m_children;
        
        /// <summary>
        /// Backing field for Skin.
        /// </summary>
        private System.Nullable<int> m_skin;
        
        /// <summary>
        /// Backing field for Matrix.
        /// </summary>
        private System.Collections.Generic.List<float> m_matrix = new System.Collections.Generic.List<float>(new float[] {
                    1F,
                    0F,
                    0F,
                    0F,
                    0F,
                    1F,
                    0F,
                    0F,
                    0F,
                    0F,
                    1F,
                    0F,
                    0F,
                    0F,
                    0F,
                    1F});
        
        /// <summary>
        /// Backing field for Mesh.
        /// </summary>
        private System.Nullable<int> m_mesh;
        
        /// <summary>
        /// Backing field for Rotation.
        /// </summary>
        private System.Collections.Generic.List<float> m_rotation = new System.Collections.Generic.List<float>(new float[] {
                    0F,
                    0F,
                    0F,
                    1F});
        
        /// <summary>
        /// Backing field for Scale.
        /// </summary>
        private System.Collections.Generic.List<float> m_scale = new System.Collections.Generic.List<float>(new float[] {
                    1F,
                    1F,
                    1F});
        
        /// <summary>
        /// Backing field for Translation.
        /// </summary>
        private System.Collections.Generic.List<float> m_translation = new System.Collections.Generic.List<float>(new float[] {
                    0F,
                    0F,
                    0F});
        
        /// <summary>
        /// Backing field for Weights.
        /// </summary>
        private System.Collections.Generic.List<float> m_weights;
        
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
        /// The index of the camera referenced by this node.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("camera")]
        public System.Nullable<int> Camera {
            get {
                return this.m_camera;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("Camera", value, "Expected value to be greater than or equal to 0");
                }
                this.m_camera = value;
            }
        }
        
        /// <summary>
        /// The indices of this node's children.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("children")]
        public System.Collections.Generic.List<int> Children {
            get {
                return this.m_children;
            }
            set {
                if ((value == null)) {
                    this.m_children = value;
                    return;
                }
                if ((value.Count < 1u)) {
                    throw new System.ArgumentException("List not long enough");
                }
                int index = 0;
                for (index = 0; (index < value.Count); index = (index + 1)) {
                    if ((value[index] < 0)) {
                        throw new System.ArgumentOutOfRangeException();
                    }
                }
                this.m_children = value;
            }
        }
        
        /// <summary>
        /// The index of the skin referenced by this node.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("skin")]
        public System.Nullable<int> Skin {
            get {
                return this.m_skin;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("Skin", value, "Expected value to be greater than or equal to 0");
                }
                this.m_skin = value;
            }
        }
        
        /// <summary>
        /// A floating-point 4x4 transformation matrix stored in column-major order.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("matrix")]
        public System.Collections.Generic.List<float> Matrix {
            get {
                return this.m_matrix;
            }
            set {
                if ((value.Count < 16u)) {
                    throw new System.ArgumentException("List not long enough");
                }
                if ((value.Count > 16u)) {
                    throw new System.ArgumentException("Array too long");
                }
                this.m_matrix = value;
            }
        }
        
        /// <summary>
        /// The index of the mesh in this node.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("mesh")]
        public System.Nullable<int> Mesh {
            get {
                return this.m_mesh;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("Mesh", value, "Expected value to be greater than or equal to 0");
                }
                this.m_mesh = value;
            }
        }
        
        /// <summary>
        /// The node's unit quaternion rotation in the order (x, y, z, w), where w is the scalar.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("rotation")]
        public System.Collections.Generic.List<float> Rotation {
            get {
                return this.m_rotation;
            }
            set {
                if ((value.Count < 4u)) {
                    throw new System.ArgumentException("List not long enough");
                }
                if ((value.Count > 4u)) {
                    throw new System.ArgumentException("Array too long");
                }
                int index = 0;
                for (index = 0; (index < value.Count); index = (index + 1)) {
                    if ((value[index] < -1D)) {
                        throw new System.ArgumentOutOfRangeException();
                    }
                }
                for (index = 0; (index < value.Count); index = (index + 1)) {
                    if ((value[index] > 1D)) {
                        throw new System.ArgumentOutOfRangeException();
                    }
                }
                this.m_rotation = value;
            }
        }
        
        /// <summary>
        /// The node's non-uniform scale, given as the scaling factors along the x, y, and z axes.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("scale")]
        public System.Collections.Generic.List<float> Scale {
            get {
                return this.m_scale;
            }
            set {
                if ((value.Count < 3u)) {
                    throw new System.ArgumentException("List not long enough");
                }
                if ((value.Count > 3u)) {
                    throw new System.ArgumentException("Array too long");
                }
                this.m_scale = value;
            }
        }
        
        /// <summary>
        /// The node's translation along the x, y, and z axes.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("translation")]
        public System.Collections.Generic.List<float> Translation {
            get {
                return this.m_translation;
            }
            set {
                if ((value.Count < 3u)) {
                    throw new System.ArgumentException("List not long enough");
                }
                if ((value.Count > 3u)) {
                    throw new System.ArgumentException("Array too long");
                }
                this.m_translation = value;
            }
        }
        
        /// <summary>
        /// The weights of the instantiated morph target. The number of array elements **MUST** match the number of morph targets of the referenced mesh. When defined, `mesh` **MUST** also be defined.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("weights")]
        public System.Collections.Generic.List<float> Weights {
            get {
                return this.m_weights;
            }
            set {
                if ((value == null)) {
                    this.m_weights = value;
                    return;
                }
                if ((value.Count < 1u)) {
                    throw new System.ArgumentException("List not long enough");
                }
                this.m_weights = value;
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
        
        public bool ShouldSerializeCamera() {
            return ((m_camera == null) 
                        == false);
        }
        
        public bool ShouldSerializeChildren() {
            return ((m_children == null) 
                        == false);
        }
        
        public bool ShouldSerializeSkin() {
            return ((m_skin == null) 
                        == false);
        }
        
        public bool ShouldSerializeMatrix() {
            return (m_matrix.SequenceEqual(new System.Collections.Generic.List<float>(new float[] {
                            1F,
                            0F,
                            0F,
                            0F,
                            0F,
                            1F,
                            0F,
                            0F,
                            0F,
                            0F,
                            1F,
                            0F,
                            0F,
                            0F,
                            0F,
                            1F})) == false);
        }
        
        public bool ShouldSerializeMesh() {
            return ((m_mesh == null) 
                        == false);
        }
        
        public bool ShouldSerializeRotation() {
            return (m_rotation.SequenceEqual(new System.Collections.Generic.List<float>(new float[] {
                            0F,
                            0F,
                            0F,
                            1F})) == false);
        }
        
        public bool ShouldSerializeScale() {
            return (m_scale.SequenceEqual(new System.Collections.Generic.List<float>(new float[] {
                            1F,
                            1F,
                            1F})) == false);
        }
        
        public bool ShouldSerializeTranslation() {
            return (m_translation.SequenceEqual(new System.Collections.Generic.List<float>(new float[] {
                            0F,
                            0F,
                            0F})) == false);
        }
        
        public bool ShouldSerializeWeights() {
            return ((m_weights == null) 
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
        
        public class NodeConverter : JsonConverter<Node> {
            
            public override bool CanConvert(System.Type typeToConvert) {
                return (typeof(Node) == typeToConvert);
            }
            
            public override Node Read(ref Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options) {
                if ((reader.TokenType != JsonTokenType.StartObject)) {
                    throw new JsonException();
                }
                Node foo = new Node();
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
                        if ((propertyName == "camera")) {
                            foo.Camera = System.Text.Json.JsonSerializer.Deserialize<System.Nullable<int>>(ref reader, options);
                        }
                        if ((propertyName == "children")) {
                            foo.Children = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.List<int>>(ref reader, options);
                        }
                        if ((propertyName == "skin")) {
                            foo.Skin = System.Text.Json.JsonSerializer.Deserialize<System.Nullable<int>>(ref reader, options);
                        }
                        if ((propertyName == "matrix")) {
                            foo.Matrix = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.List<float>>(ref reader, options);
                        }
                        if ((propertyName == "mesh")) {
                            foo.Mesh = System.Text.Json.JsonSerializer.Deserialize<System.Nullable<int>>(ref reader, options);
                        }
                        if ((propertyName == "rotation")) {
                            foo.Rotation = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.List<float>>(ref reader, options);
                        }
                        if ((propertyName == "scale")) {
                            foo.Scale = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.List<float>>(ref reader, options);
                        }
                        if ((propertyName == "translation")) {
                            foo.Translation = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.List<float>>(ref reader, options);
                        }
                        if ((propertyName == "weights")) {
                            foo.Weights = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.List<float>>(ref reader, options);
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
            
            public override void Write(System.Text.Json.Utf8JsonWriter writer, Node value, System.Text.Json.JsonSerializerOptions options) {
                writer.WriteStartObject();
                if (value.ShouldSerializeCamera()) {
                    writer.WritePropertyName("camera");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Camera, options);
                }
                if (value.ShouldSerializeChildren()) {
                    writer.WritePropertyName("children");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Children, options);
                }
                if (value.ShouldSerializeSkin()) {
                    writer.WritePropertyName("skin");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Skin, options);
                }
                if (value.ShouldSerializeMatrix()) {
                    writer.WritePropertyName("matrix");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Matrix, options);
                }
                if (value.ShouldSerializeMesh()) {
                    writer.WritePropertyName("mesh");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Mesh, options);
                }
                if (value.ShouldSerializeRotation()) {
                    writer.WritePropertyName("rotation");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Rotation, options);
                }
                if (value.ShouldSerializeScale()) {
                    writer.WritePropertyName("scale");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Scale, options);
                }
                if (value.ShouldSerializeTranslation()) {
                    writer.WritePropertyName("translation");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Translation, options);
                }
                if (value.ShouldSerializeWeights()) {
                    writer.WritePropertyName("weights");
                    System.Text.Json.JsonSerializer.Serialize(writer, value.Weights, options);
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
