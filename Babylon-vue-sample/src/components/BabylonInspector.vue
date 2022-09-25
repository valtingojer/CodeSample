<template>
  <div :class="['control-panel', `${classes()}`, `${state}`]">
    <div class="arrow" v-on:click="toggleControlPanel">
      <span v-if="isOpen">&#xbb;</span>
      <span v-else>&#xab;</span>
    </div>

    <div
      class="admin-controls"
      v-if="!hideTransform"
      style="margin-bottom: 24px"
    >
      <div v-if="transformItems && transformItems.length > 0">
        <h2>Select an object</h2>
        <select v-model="transformItem" v-on:change="changeTransformItem">
          <option
            v-for="item in transformItems"
            :key="item.key"
            :selected="item.selected"
            :value="item.name"
          >
            {{ item.name }}
          </option>
        </select>
        <div v-if="transformItem">
          <div>
            <h2>Change visibility</h2>
            <select
              v-on:click="toggleVisibility"
              v-model="toggleVisibilityValue"
            >
              <option value="show" selected>Show</option>
              <option value="hide">Hide</option>
            </select>
          </div>
          <div>
            <h2>Transform</h2>
            <h3>Scale</h3>
            <input
              class="position-input"
              type="number"
              v-on:change="scaleItem"
              v-model="itemScale.x"
            />
            <input
              class="position-input"
              type="number"
              v-on:change="scaleItem"
              v-model="itemScale.y"
            />
            <input
              class="position-input"
              type="number"
              v-on:change="scaleItem"
              v-model="itemScale.z"
            />

            <h3>Position</h3>
            <input
              class="position-input"
              type="number"
              v-on:change="moveItem"
              v-model="itemPosition.x"
            />
            <input
              class="position-input"
              type="number"
              v-on:change="moveItem"
              v-model="itemPosition.y"
            />
            <input
              class="position-input"
              type="number"
              v-on:change="moveItem"
              v-model="itemPosition.z"
            />

            <h3>Rotation</h3>
            <input
              class="position-input"
              type="number"
              v-on:change="rotateItem"
              v-model="itemRotation.x"
            />
            <input
              class="position-input"
              type="number"
              v-on:change="rotateItem"
              v-model="itemRotation.y"
            />
            <input
              class="position-input"
              type="number"
              v-on:change="rotateItem"
              v-model="itemRotation.z"
            />
          </div>
        </div>
      </div>
      <div v-else>There is no items to show at transform</div>
    </div>

    <div class="user-controls">
      <div>
        <div v-if="exchangeableAssets && exchangeableAssets.length > 0" style="margin-bottom: 16px">
          <div v-for="(item, index) in exchangeableAssets" :key="index">
            <h2>Change {{ item.name }}</h2>
            <select v-model="exchangeable[`${item.name}`]" v-on:change="changeAsset(`${item.name}`)">
              <option></option>
              <option
                v-for="asset in item.groups"
                :key="asset.name"
                :selected="exchangeable[`${item.name}`] == asset.name"
                :value="asset.name"
              >
                {{ asset.name }}
              </option>
            </select>
          </div>
        </div>

        <div>
          <div v-if="textureableAssets && textureableAssets.length > 0">
            <h2>Select target</h2>
            <select v-model="textureableAsset" v-on:change="selectMaterialTarget">
              <option></option>
              <option
                v-for="item in textureableAssets"
                :key="item.key"
                :selected="textureableAsset == item.name"
                :value="item.name"
              >
                {{ item.name }}
              </option>
            </select>
          </div>
          <div v-if="textureableAsset">
            <div v-if="textures && textures.length > 0">
              <h2>Change texture</h2>
              <select v-model="texture" v-on:change="changeTexture">
                <option></option>
                <option
                  v-for="item in textures"
                  :key="item.key"
                  :selected="texture == item.name"
                  :value="item.name"
                >
                  {{ item.name }}
                </option>
              </select>
            </div>

            <div>
              <h2>Change color</h2>
              <input
                  style="width: 25%"
                  placeholder="R"
                  type="number"
                  v-on:change="changeColor"
                  v-model="materialColor.r"
                  min="0"
                  max="255"
                />
                <input
                  style="width: 25%"
                  placeholder="G"
                  type="number"
                  v-on:change="changeColor"
                  v-model="materialColor.g"
                  min="0"
                  max="255"
                />
                <input
                  style="width: 25%"
                  placeholder="B"
                  type="number"
                  v-on:change="changeColor"
                  v-model="materialColor.b"
                  min="0"
                  max="255"
                />
                <input
                  style="width: 25%"
                  placeholder="A"
                  type="number"
                  v-on:change="changeColor"
                  v-model="materialColor.a"
                  min="0"
                  max="255"
                />
            </div>

          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style>
.control-panel {
  position: absolute;
  width: 300px;
  height: 100%;
  background-color: #ffff;
  float: right;
  transition: all 0.5s;
  color: black;
  z-index: 9;
  right: 0;
  padding: 16px;
}
.control-panel.open {
  right: 0px;
}
.control-panel.closed {
  right: -300px;
}

.control-panel .arrow {
  position: absolute;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 50px;
  height: 50px;
  background-color: #ffff;
  border-start-start-radius: 8px;
  border-end-start-radius: 8px;
  z-index: 9;
  left: -50px;
  top: 0;
}
.control-panel .arrow span {
  color: #000;
  font-size: 50px;
  line-height: 40px;
  margin-top: -10px;
}
</style>

<script>
import * as BabylonHelper from "@/helpers/BabylonHelper.js";

export default {
  props: {
    class: {
      type: String,
      default: "",
    },
    hideTransform: {
      type: Boolean,
      default: true,
    },
    loadShapes: {
      type: Array,
      default: [],
    },
    loadAssets: {
      type: Array,
      default: [],
    },
    exchangeableAssets: {
      type: Array,
      default: [],
    },
    textureableAssets: {
      type: Array,
      default: [],
    },
  },
  //   components: {},
  data() {
    return {
      scaleFactor: 100,
      positionFactor: 100,
      state: "closed",
      isOpen: false,
      transformItem: null,
      transformItems: [],
      toggleVisibilityValue: "",
      itemScale: {
        x: 1,
        y: 1,
        z: 1,
      },
      itemPosition: {
        x: 0,
        y: 0,
        z: 0,
      },
      itemRotation: {
        x: 0,
        y: 0,
        z: 0,
      },
      textureableAsset: "",
      textures: [],
      texture: "",
      materialColor: {
        r: 255,
        g: 255,
        b: 255,
        a: 255,
      },
      exchangeable:{},
    };
  },
  //   computed: {},
  methods: {
    classes() {
      return this.class;
    },
    toggleControlPanel() {
      this.isOpen = !this.isOpen;
      this.state = this.isOpen ? "open" : "closed";
    },
    getAssetsArray() {
      let items = [];
      this.loadAssets.forEach((asset) => {
        let a = JSON.parse(JSON.stringify(asset));
        a["selected"] = false;
        items.push(a);
      });

      this.loadShapes.forEach((shape) => {
        let a = JSON.parse(JSON.stringify(shape));
        a["selected"] = false;
        items.push(a);
      });

      items = items.sort((a, b) => {
        let result = 0;
        if (a.name < b.name) result = -1;
        if (a.name > b.name) result = 1;
        return result;
      });

      return items;
    },
    getHierarchyItem(item) {
      return window["hierarchy"][item] || null;
    },
    toggleVisibility() {
      if (this.toggleVisibilityValue && this.transformItem) {
        let item = this.getHierarchyItem(this.transformItem);

        if (!item) {
          console.log(`Item ${this.transformItem} not found in hierarchy`);
          return;
        }

        BabylonHelper.toggleVisibility(
          item,
          this.toggleVisibilityValue == "show"
        );
      }
    },

    changeTransformItem() {
      if (this.transformItem) {
        let item = this.getHierarchyItem(this.transformItem);
        if (item) {
          this.itemScale = {
            x: item.scaling.x * this.scaleFactor,
            y: item.scaling.y * this.scaleFactor,
            z: item.scaling.z * this.scaleFactor,
          };

          this.itemPosition = {
            x: item.position.x * this.positionFactor,
            y: item.position.y * this.positionFactor,
            z: item.position.z * this.positionFactor,
          };

          this.itemRotation = {
            x: item.rotation.x,
            y: item.rotation.y,
            z: item.rotation.z,
          };
        }else{
          console.log(`Item ${this.transformItem} not found in hierarchy`);
        }
      }else{
        console.log(`Item ${this.transformItem} is empty`)
      }
    },

    scaleItem() {
      if (this.transformItem) {
        let item = this.getHierarchyItem(this.transformItem);
        let newScale = {
          x: this.itemScale.x || 0,
          y: this.itemScale.y || 0,
          z: this.itemScale.z || 0,
        };

        item.scaling = new BABYLON.Vector3(
          newScale.x > 0 ? newScale.x / this.scaleFactor : 0,
          newScale.y > 0 ? newScale.y / this.scaleFactor : 0,
          newScale.z > 0 ? newScale.z / this.scaleFactor : 0
        );
      }
    },

    moveItem() {
      if (this.transformItem) {
        let item = this.getHierarchyItem(this.transformItem);
        let newPosition = {
          x: this.itemPosition.x || 0,
          y: this.itemPosition.y || 0,
          z: this.itemPosition.z || 0,
        };

        item.position = new BABYLON.Vector3(
          newPosition.x > 0 ? newPosition.x / this.positionFactor : 0,
          newPosition.y > 0 ? newPosition.y / this.positionFactor : 0,
          newPosition.z > 0 ? newPosition.z / this.positionFactor : 0
        );
      }
    },

    rotateItem() {
      if (this.transformItem) {
        let item = this.getHierarchyItem(this.transformItem);
        let newRotation = {
          x: this.itemRotation.x || 0,
          y: this.itemRotation.y || 0,
          z: this.itemRotation.z || 0,
        };

        item.rotation = new BABYLON.Vector3(
          BabylonHelper.degreesToRadians(newRotation.x),
          BabylonHelper.degreesToRadians(newRotation.y),
          BabylonHelper.degreesToRadians(newRotation.z)
        );
      }
    },

    
    selectMaterialTarget() {
      this.textures = []
      this.texture = ""

      if(!this.textureableAsset) return;

      let target = this.textureableAsset;
      let mesh = this.getHierarchyItem(target);

      if (mesh) {
        let material = mesh.material;
        this.textures = this.textureableAssets.filter( ({ name }) => name == this.textureableAsset )[0].getTextures();

        if (material) {
          if (material.diffuseColor) {
            this.materialColor = {
              r: (material.diffuseColor.r || 0) * 255,
              g: (material.diffuseColor.g || 0) * 255,
              b: (material.diffuseColor.b || 0) * 255,
              a: (material.diffuseColor.a || 0) * 255,
            };
          }

          if (material.diffuseTexture) {
            let matName = material.diffuseTexture.name;
            this.texture = matName;
          }
        }
      }else{
        console.log(`Item ${target} not found in hierarchy`);
      }
    },


    
    changeTexture() {
      console.log("change texture")
      let target = this.textureableAsset
      let mesh = this.getHierarchyItem(target)

      if (mesh) {
        let {url} = this.textures.filter(
            (x) => x.name == this.texture
          )[0] || {url:null};

          if(url){
            BabylonHelper.changeTexture(mesh, url, this.scene);
          }else{
            console.log("clear texture")
            BabylonHelper.clearTextures(mesh);
          }
      }else{
        console.log(`Item ${target} not found in hierarchy`);
      }
    },


    
    changeColor() {
      let target = this.textureableAsset
      let mesh = this.getHierarchyItem(target)


      if (mesh) {
        BabylonHelper.changeColorRGBA(mesh, this.materialColor, this.scene)
      }else{
        console.log(`Item ${target} not found in hierarchy`);
      }


    },


    changeAsset(group){
      let selected = this.exchangeable[group]
      let pack = this.exchangeableAssets.filter( ({ name }) => name == group )[0];

      if(!selected) return;

      if(pack){
        let { items: disableItems, groups } = pack;
        let { items: enableItems } = groups.filter( ({ name }) => name == selected )[0];

        disableItems.forEach( item => {
          let mesh = this.getHierarchyItem(item)
          if(mesh){
            BabylonHelper.hideObject(mesh)
          }else{
            console.log(`Item ${item} not found in hierarchy`);
          }
        })

        enableItems.forEach( item => {
          let mesh = this.getHierarchyItem(item)
          if(mesh){
            BabylonHelper.showObject(mesh)
          }else{
            console.log(`Item ${item} not found in hierarchy`);
          }
        })

      }else{
        console.log(`Item ${group} not found in exchangeableAssets`);
      }
    }

  },
  // beforeCreate() {},
  // created() {},
  // beforeMount() {},
  mounted() {
    this.transformItems = this.getAssetsArray();
  },
  // watch:{}
  // beforeUpdate() {},
  // updated() {},
  // activated() {},
  // deactivated() {},
  // beforeUnmount() {},
  // unmounted() {},
  // errorCaptured() {},
  // renderTracked() {},
  // renderTriggered() {},
};
</script>
