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
    
    
    public class Skin {
        
        /// <summary>
        /// Backing field for InverseBindMatrices.
        /// </summary>
        private System.Nullable<int> m_inverseBindMatrices;
        
        /// <summary>
        /// Backing field for Skeleton.
        /// </summary>
        private System.Nullable<int> m_skeleton;
        
        /// <summary>
        /// Backing field for Joints.
        /// </summary>
        private System.Collections.Generic.List<int> m_joints;
        
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
        /// The index of the accessor containing the floating-point 4x4 inverse-bind matrices.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("inverseBindMatrices")]
        public System.Nullable<int> InverseBindMatrices {
            get {
                return this.m_inverseBindMatrices;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("InverseBindMatrices", value, "Expected value to be greater than or equal to 0");
                }
                this.m_inverseBindMatrices = value;
            }
        }
        
        /// <summary>
        /// The index of the node used as a skeleton root.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("skeleton")]
        public System.Nullable<int> Skeleton {
            get {
                return this.m_skeleton;
            }
            set {
                if ((value < 0)) {
                    throw new System.ArgumentOutOfRangeException("Skeleton", value, "Expected value to be greater than or equal to 0");
                }
                this.m_skeleton = value;
            }
        }
        
        /// <summary>
        /// Indices of skeleton nodes, used as joints in this skin.
        /// </summary>
        [System.Text.Json.Serialization.JsonRequiredAttribute()]
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("joints")]
        public System.Collections.Generic.List<int> Joints {
            get {
                return this.m_joints;
            }
            set {
                if ((value == null)) {
                    this.m_joints = value;
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
                this.m_joints = value;
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
        
        public bool ShouldSerializeInverseBindMatrices() {
            return ((m_inverseBindMatrices == null) 
                        == false);
        }
        
        public bool ShouldSerializeSkeleton() {
            return ((m_skeleton == null) 
                        == false);
        }
        
        public bool ShouldSerializeJoints() {
            return ((m_joints == null) 
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