<template>
  <div id="babylon-render" ref="babylonRender" :class="['babylon-render', `${classes()}`]">
    <div class="babylon-loader-background" v-if="isLoading"></div>

    <div class="babylon-loading" v-if="isLoading">
      <div style="text-align: center">
      <img src="@/assets/images/loading.gif" alt="loading" /><br/>
      <h1 style="margin-top: -200px">{{percentageLoaded}}%</h1>
      </div>
    </div>

    <canvas :id="id" class="babylon-canvas"></canvas>
  </div>
</template>

<style>
.babylon-loader-background {
  position: absolute;
  width: 100%;
  height: 100%;
  z-index: 9;
  background-color: rgb(0, 0, 0);
  top: 0;
  left: 0;
}
.babylon-loading {
  width: 100vw;
  height: 100vh;
  z-index: 9999;
  background-color: rgb(0, 0, 0);
  display: flex;
  justify-content: center;
  align-items: center;
  position: fixed;
  top: 0;
  left: 0;
  opacity: 0.6;
}
.babylon-canvas,
.babylon-render {
  width: 100%;
  height: 100%;
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
    id: {
      type: String,
      default: "babylon-canvas",
    },
    assets: {
      type: Object,
      default: null,
    },
    loadShapes: {
      type: Array,
      default: [],
    },
    loadAssets: {
      type: Array,
      default: [],
    },
  },
  // //   components: {},
  data() {
    return {
      isLoading: true,
      percentageLoaded: 0,
      config: {
        engine: {
          preserveDrawingBuffer: true,
          stencil: true,
          disableWebGL2Support: false,
        },
      },
      canvas: null,
      engine: null,
      scene: null,
      hierarchy: {
        camera: null,
        ground: null,
        light: null,
      },
    };
  },
  //   computed: {},
  methods: {
    classes() { return this.class; },
    lockCameraMove() {
      BabylonHelper.mainCamera().setTargetToZero(this.hierarchy.camera);
    },
    lockCameraRotation() {},
    ExposeHierarchyAndScene() {
      window["hierarchy"] = this.hierarchy;
      window["scene"] = this.scene;
    },
    update() {
      this.scene.render();
      this.lockCameraMove();
      this.lockCameraRotation();
      this.ExposeHierarchyAndScene();
    },
    onResize() {
      if (this.engine) this.engine.resize();
    },
    init() {
      this.canvas = document.getElementById(this.id);

      this.engine = new BABYLON.Engine(this.canvas, true, this.config.engine);

      this.scene = BabylonHelper.createScene(this.engine);

      this.hierarchy.camera = BabylonHelper.mainCamera().create(
        this.scene,
        this.canvas
      );

      this.hierarchy.light = BabylonHelper.setMainLight(this.scene);

      this.engine.runRenderLoop(this.update);

      window.addEventListener("resize", this.onResize);
    },

    async assetLoader(
      scene,
      key,
      name,
      visible,
      reference,
      onSuccess,
      onError,
      data
    ) {
      if (this.assets == null) {
        console.log(`No assets to load`);
        return;
      }

      var f_assets = this.assets.filter((asset) => asset.key == key);

      if (!f_assets && f_assets.length == 0) {
        console.log(`No asset named ${name} to load`);
        return;
      }

      var first = f_assets[0];

      //deep clone first as asset so we do not fire watch event
      var asset = JSON.parse(JSON.stringify(first));

      if (reference) {
        let accumulatedPosition = asset.position;
        if (this.hierarchy[reference] && this.hierarchy[reference].position) {
          accumulatedPosition = BabylonHelper.sumVector3([
            asset.position,
            this.hierarchy[reference].position,
          ]);
        }

        asset.position = new BABYLON.Vector3(
          accumulatedPosition.x,
          accumulatedPosition.y,
          accumulatedPosition.z
        );
      }

      this.hierarchy[name] = new BABYLON.TransformNode(name);

      await BabylonHelper.AddMesh(
        {
          name: name,
          scene: scene,
          asset: asset,
          onSuccess: onSuccess,
          onError: onError,
          visible: visible,
          data: data,
        },
        this.hierarchy[name]
      );
    },

    async loadShapesMethod() {
      if (this.loadShapes && this.loadShapes.length > 0) {
        this.loadShapes.forEach((shape) => {
          let s = BABYLON.Mesh[`${shape.method}`](
            shape.name,
            shape.size,
            this.scene
          );
          s.position = new BABYLON.Vector3(
            shape.position.x,
            shape.position.y,
            shape.position.z
          );
          s.rotation = new BABYLON.Vector3(
            shape.rotation.x,
            shape.rotation.y,
            shape.rotation.z
          );
          s.scaling = new BABYLON.Vector3(
            shape.scaling.x,
            shape.scaling.y,
            shape.scaling.z
          );

          let materialNameOrUrl = `${shape.name}_material`;

          s.material = new BABYLON.StandardMaterial(
            materialNameOrUrl,
            this.scene
          );

          if (shape.textures && shape.textures.length > 0) {
            let selected = shape.textures.filter(
              (m) => m.selected == "selected"
            );

            if (selected && selected.length > 0) {
              let first = selected[0];
              s.material.diffuseTexture = new BABYLON.Texture(first.url, this.scene)
            }
          }



          if (shape.color) {
            let c = {
              r: shape.color.r || 0,
              g: shape.color.g || 0,
              b: shape.color.b || 0,
              a: shape.color.a || 0,
            };

            s.material.diffuseColor = new BABYLON.Color4(
              c.r > 0 ? c.r / 255 : 0,
              c.g > 0 ? c.g / 255 : 0,
              c.b > 0 ? c.b / 255 : 0,
              c.a > 0 ? c.a / 255 : 0
            );

            s.material.alpha = c.a > 0 ? c.a / 255 : 0;
          }

          if (shape.visible == false) {
            s.isVisible = false;
          }

          this.hierarchy[shape.name] = s;

          this.recalculateProgress(1);
        });
      }
    },
    async loadMeshesMethod() {
      if (this.loadAssets && this.loadAssets.length > 0) {
        for (let i = 0; i < this.loadAssets.length; i++) {
          let scene = this.scene;
          let name = this.loadAssets[i].name;
          let key = this.loadAssets[i].key;
          let visible = this.loadAssets[i].visible;
          let reference = this.loadAssets[i].reference;
          let onSuccess = this.loadAssets[i].onSuccess || null;
          let onError = this.loadAssets[i].onError || null;

          await this.assetLoader(
            scene,
            key,
            name,
            visible,
            reference,
            onSuccess,
            onError,
            this.loadAssets[i]
          );

          this.recalculateProgress(1);
        }
      }
    },
    async loadAssetsMethod() {
      this.isLoading = true;
      await this.loadShapesMethod();
      await this.loadMeshesMethod();
      this.isLoading = false;
    },
    async recalculateProgress(loadedNow) {
      let assetsToLoad = this.loadAssets.length;
      let ShapesToLoad = this.loadShapes.length;
      let total = assetsToLoad + ShapesToLoad;
      
      let loaded = window["renderLoaded"] || 0
      loaded += loadedNow;
      window["renderLoaded"] = loaded;

      let progress = (loaded / total) * 100;
      progress = parseFloat(progress.toFixed(2));

      this.percentageLoaded = progress;
    },
  },
  //   beforeCreate() {},
  created() {
    this.isLoading = true;
  },
  //   beforeMount() {},
  async mounted() {
    this.isLoading = true;
    this.init();
    await this.loadAssetsMethod();
    this.isLoading = false;
  },
  watch: {
    assets: {
      handler: function (val, oldVal) {
        console.log("assets changed, cleanup scene and reload");
        this.loadAssetsMethod();
      },
      deep: true,
    },
  },
  //   beforeUpdate() {},
  //   updated() {},
  //   activated() {},
  //   deactivated() {},
  //   beforeUnmount() {},
  //   unmounted() {},
  //   errorCaptured() {},
  //   renderTracked() {},
  //   renderTriggered() {},
};
</script>
