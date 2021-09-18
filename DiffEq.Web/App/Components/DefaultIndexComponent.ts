import { defineComponent } from "vue"

export default defineComponent({
    props: {
        isenabled: {
            type: Boolean,
            required: true,
            default: true
        }
    },
    emits: {

    },
    setup(props, { emit }) {
        return {

        }
    },
    template: "#default-index-template"
})