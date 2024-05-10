<template>
    <label class="form-label">{{label}}</label>
    <div>
        <ejs-slider   :required="required"
                      :default="default"
                      :show-buttons="showButtons"
                      :step="step"
                      :enabled="enabled"
                      :min="min"
                      :max="max"
                      v-model="computedValue"
                      :ticks="ticks">
            {{computedValue}}
        </ejs-slider>
    </div>
</template>

<script setup lang="ts">
    import { ref, computed } from 'vue';
    import { SliderComponent as EjsSlider } from "@syncfusion/ej2-vue-inputs";
    import { SliderPlugin } from "@syncfusion/ej2-vue-inputs";

    defineOptions({
        inheritAttrs: false
    });

    const props = defineProps({
        required: {
            type: Boolean,
        },
        step: {
            type: Number,
            default: 1,
        },
        default: {
            type: Number,
        },
        enabled: {
            type: Boolean,
        },
        min: {
            type: Number,
            required: true,
        },
        max: {
            type: Number,
            required: true,
        },
        modelValue: {
            type: Number,
            required: true,
        },
        showButtons: {
            type: Boolean,
        },
        ticks: {
            type: Object,
        },
    });

    const emit = defineEmits(['update:modelValue']);

    const computedValue = computed({
        get: () => props.modelValue,
        set: (newValue) => {
            emit('update:modelValue', newValue);
        }
    });

</script>