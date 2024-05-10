<template>
    <label v-if="label" class="form-label">{{label}}</label>
    <div>
        <dx-color-box :input-attr="inputAttr"
                      :disabled="disabled"
                      v-model="computedValue">
        </dx-color-box>
    </div>
</template>

<script setup lang="ts">
    import {
        computed,
    } from 'vue';
    import DxColorBox from 'devextreme-vue/color-box';


    defineOptions({
        inheritAttrs: false
    });

    const props = defineProps({
        required: {
            type: Boolean,
        },
        disabled: {
            type: Boolean,
        },
        modelValue: {
            type: String,
            required: true,
        },
        inputAttr: {
            type: Object,
            default: { 'aria-label': 'DxColorBox' },
        },
        label: {
            type: String,
        },
    });

    const emit = defineEmits(['update:modelValue']);

    const computedValue = computed({
        get: () => props.modelValue,
        set: (newValue) => {
            emit('update:modelValue', newValue);
        },
    });
</script>