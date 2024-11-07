//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace FrostyMeshPlugin.Gltf.AutoGenerated {
    using System.Linq;
    using System.Runtime.Serialization;
    
    
    public class Mesh {
        
        /// <summary>
        /// Backing field for Primitives.
        /// </summary>
        private List<MeshPrimitive> m_primitives;
        
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
        /// An array of primitives, each defining geometry to be rendered.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("primitives")]
        public List<MeshPrimitive> Primitives {
            get {
                return this.m_primitives;
            }
            set {
                if ((value == null)) {
                    this.m_primitives = value;
                    return;
                }
                if ((value.Count < 1u)) {
                    throw new System.ArgumentException("List not long enough");
                }
                this.m_primitives = value;
            }
        }
        
        /// <summary>
        /// Array of weights to be applied to the morph targets. The number of array elements **MUST** match the number of morph targets.
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
        
        public bool ShouldSerializePrimitives() {
            return ((m_primitives == null) 
                        == false);
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
    }
}