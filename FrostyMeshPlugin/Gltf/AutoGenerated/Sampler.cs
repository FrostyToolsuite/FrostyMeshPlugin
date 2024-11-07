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
    
    
    public class Sampler {
        
        /// <summary>
        /// Backing field for MagFilter.
        /// </summary>
        private System.Nullable<MagFilterEnum> m_magFilter;
        
        /// <summary>
        /// Backing field for MinFilter.
        /// </summary>
        private System.Nullable<MinFilterEnum> m_minFilter;
        
        /// <summary>
        /// Backing field for WrapS.
        /// </summary>
        private WrapSEnum m_wrapS = WrapSEnum.REPEAT;
        
        /// <summary>
        /// Backing field for WrapT.
        /// </summary>
        private WrapTEnum m_wrapT = WrapTEnum.REPEAT;
        
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
        /// Magnification filter.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("magFilter")]
        public System.Nullable<MagFilterEnum> MagFilter {
            get {
                return this.m_magFilter;
            }
            set {
                this.m_magFilter = value;
            }
        }
        
        /// <summary>
        /// Minification filter.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("minFilter")]
        public System.Nullable<MinFilterEnum> MinFilter {
            get {
                return this.m_minFilter;
            }
            set {
                this.m_minFilter = value;
            }
        }
        
        /// <summary>
        /// S (U) wrapping mode.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("wrapS")]
        public WrapSEnum WrapS {
            get {
                return this.m_wrapS;
            }
            set {
                this.m_wrapS = value;
            }
        }
        
        /// <summary>
        /// T (V) wrapping mode.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("wrapT")]
        public WrapTEnum WrapT {
            get {
                return this.m_wrapT;
            }
            set {
                this.m_wrapT = value;
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
        
        public bool ShouldSerializeMagFilter() {
            return ((m_magFilter == null) 
                        == false);
        }
        
        public bool ShouldSerializeMinFilter() {
            return ((m_minFilter == null) 
                        == false);
        }
        
        public bool ShouldSerializeWrapS() {
            return ((m_wrapS == WrapSEnum.REPEAT) 
                        == false);
        }
        
        public bool ShouldSerializeWrapT() {
            return ((m_wrapT == WrapTEnum.REPEAT) 
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
        
        public enum MagFilterEnum {
            
            NEAREST = 9728,
            
            LINEAR = 9729,
        }
        
        public enum MinFilterEnum {
            
            NEAREST = 9728,
            
            LINEAR = 9729,
            
            NEAREST_MIPMAP_NEAREST = 9984,
            
            LINEAR_MIPMAP_NEAREST = 9985,
            
            NEAREST_MIPMAP_LINEAR = 9986,
            
            LINEAR_MIPMAP_LINEAR = 9987,
        }
        
        public enum WrapSEnum {
            
            CLAMP_TO_EDGE = 33071,
            
            MIRRORED_REPEAT = 33648,
            
            REPEAT = 10497,
        }
        
        public enum WrapTEnum {
            
            CLAMP_TO_EDGE = 33071,
            
            MIRRORED_REPEAT = 33648,
            
            REPEAT = 10497,
        }
    }
}
